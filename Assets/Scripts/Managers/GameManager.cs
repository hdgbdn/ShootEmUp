//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotEmUp
{
    /// <summary>
    /// All the manager instance are keep here.
    /// Using a LinkedList to achieve sigleton.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static readonly LinkedList<Manager> s_Managets = new LinkedList<Manager>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private async void Start()
        {
            DontDestroyOnLoad(gameObject);
            InitManagers();
            GameState.OnGameStateChange += OnGameStateChange;
            // Wait for one frame to let other manager finish their delegate attaching
            // not elegant
            await UniTask.DelayFrame(1);
            GameState.ChangeState(GameStateManager.GameState.MainMenu);
        }

        public static void RegisterManager(Manager manager)
        {
            if (manager == null)
            {
                Debug.LogError("Manager is invalid.");
                return;
            }

            Type type = manager.GetType();

            LinkedListNode<Manager> curNode = s_Managets.First;

            while (curNode != null)
            {
                if (curNode.Value.GetType() == type)
                {
                    Debug.LogError(string.Format("Manager type '{0}' is already exist.", type.FullName));
                    return;
                }
                curNode = curNode.Next;
            }

            s_Managets.AddLast(manager);
        }

        public static T GetManager<T>() where T : Manager
        {
            Type typ = typeof(T);

            LinkedListNode<Manager> curNode = s_Managets.First;

            while (curNode != null)
            {
                if (curNode.Value.GetType() == typ)
                {
                    return curNode.Value as T;
                }
                curNode = curNode.Next;
            }

            return null;
        }

        public static PlayerManager Player
        {
            get; private set;
        }

        public static EnemyManager Enemy
        {
            get; private set;
        }

        public static BulletManager Bullet
        {
            get; private set;
        }

        public static UIManager UI
        {
            get; private set;
        }

        public static GameStateManager GameState
        {
            get; private set;
        }

        public static ResourceManager Resource
        {
            get; private set;
        }

        private static void InitManagers()
        {
            Player = GetManager<PlayerManager>();
            Enemy = GetManager<EnemyManager>();
            Bullet = GetManager<BulletManager>();
            UI = GetManager<UIManager>();
            GameState = GetManager<GameStateManager>();
            Resource = GetManager<ResourceManager>();
        }

        public void OnGameStateChange(GameStateManager.GameState preState, GameStateManager.GameState newState)
        {
            switch (newState)
            {
                
            }
        }

        public static void EnterMenuScene()
        {
            SceneManager.LoadScene("Menu");
            UI.CreateUI("UIMainMenu");
        }

        public static void StartNewGame()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main");
            Enemy.StartGenerateEnemy();
            Player.SwapPlayer();
        }

        public static void Restart()
        {
            Time.timeScale = 1.0f;
            Enemy.StopGenerateEnemy();
            Player.ClearPlayer();
            SceneManager.LoadScene("Menu");
            UI.CreateUI("UIMainMenu");
        }

        public static void PauseGame()
        {
            UI.CreateUI("UIPauseMenu");
            Time.timeScale = 0.0f;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1.0f;
            Enemy.StartGenerateEnemy();
        }

        public static void GameOver()
        {
            UI.CreateUI("UIGameOverMenu");
            Time.timeScale = 0.0f;
        }

        public static void ExitGame() 
        {
            Application.Quit();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameState.ChangeState(GameStateManager.GameState.Pause);
            }
        }
    }
}

