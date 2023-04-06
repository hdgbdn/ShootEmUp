# ShootEmUp

This is a Shoot 'em up game I developed with a focus on a modular and scalable architecture.

## How to play

The Unity version I used for development is 2021.3.10f1.
The entrance scene of the game is in `Assets\Scenes\GameLauncher.unity`.

## System Design

### The `GameManager`

The foundation of the game is the `GameManager`, which acts as the primary manager for all other managers. Upon initialization, it registers all other managers, which are derived from the `Manager` class, into a `LinkedList<Manager>`. To ensure that there is only one instance of each type of `Manager`, the `LinkedList` checks for duplicates upon registration.I hope this modular and scalable architecture provides a flexible framework for expanding the game's features in the future.

### The flow control

The game's overall flow is managed by the `GameStateManager.ChangeState()` function. This function is responsible for updating the game's state and triggering associated managers to adjust their states accordingly.

The `GameStateManager` serves as a straightforward state machine that governs all state transitions within the game. Upon entering a specific state from a previous one, it invokes the `GameManager` to update all relevant managers. For instance, when the player clicks the `Restart` button in the pause menu, the `GameStateManager` is instructed to transition to the MainMenu state. Unlike the transition from the Idle state to the MainMenu which will call `GameManager.EnterMenuScene()`, the previous state in this case is the Pause state. Consequently, the `GameManager.Restart()` method is called to reset other active managers, such as the `EnemyManager`, which is responsible for generating enemies. This reset halts enemy generation and clears all existing enemies from the game.

### Entering the Menu

Initially, we transition from the Idle state to the MainMenu state. The `GameManager.EnterMenuScene()` method is responsible for loading the Menu scene, which serves as the stage for displaying our game's menu. Following this, the `UIManager` is called upon to create the `UIMainMenu`. At this point, the `UIMainMenu` features a single button that allows players to enter the battle scene.

### Entering the Battle

Upon clicking the `Start Game` button, the game state transitions to `Battle`. At this point, the `EnemyManager` and `PlayerManager` become active. The `PlayerManager` is responsible for controlling the player's aircraft movement, as well as managing the player's health and lives. It also takes care of spawning the player's aircraft. Meanwhile, the `EnemyManager` generates enemies, which spawn at random starting points. An object pool is utilized to manage all generated enemies efficiently.

When the player clicks and drags the mouse, their aircraft moves to the last passed point and fires bullets simultaneously. If a bullet strikes an enemy, the Collider within the EnemyAircraft is triggered, informing the `EnemyManager` that the enemy has been hit.

### Weapon System

The weapon system is divided into two components: the `BulletLauncher` and `Bullet`. The `BulletLauncher` is responsible for shooting bullets along specific trajectories, such as the `LinearBulletLauncher`, which fires bullets in a straight path. The Bullet represents the actual projectile being fired. The goal is to design bullets that follow the path provided by the launcher while also being able to move relative to their local coordinate space. Currently, only a simple `BoltBullet` has been implemented, which flies straightforward.

All emitted bullet instances are managed by the `BulletManager` through a member variable: `Dictionary<Type, ObjectPool<Bullet>>`. This object pool dictionary enables the management of various bullet types and their corresponding game objects.

### The `ObjectPool`

The `ObjectPool`, as its name suggests, manages all classes based on `MonoBehaviour` that can be displayed in the scene. Since creating and destroying objects in C# can be costly, reusability is crucial, especially for objects like bullets, which frequently undergo instantiation and destruction.

Two `LinkedList` structures are utilized to manage all instances: one for active objects and another for inactive objects. Additionally, a dictionary is used to record the duration since an object was deactivated.

During initialization, the `ObjectPool` creates a number of inactive instances and places them in the inactive list for potential use. When the `Acquire()` method is called, the `ObjectPool` retrieves an object from the inactive list if available, or creates a new instance if no suitable objects are available. When the `Release(T item)` method is called, the passed item is set to inactive and returned to the inactive list.

To prevent the inactive list from growing excessively, the inactivity time dictionary checks every object in the inactive list, comparing their inactivity time to the maximum allowed lifetime. Objects that have exceeded this limit are destroyed.
