//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using ShotEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace ShotEmUp
{
    public class GameStateManager : Manager
    {

        public delegate void OnGameStateChangeDelegate(GameState preState, GameState newState);

        public event OnGameStateChangeDelegate OnGameStateChange;

        private UIManager m_UIManager;
        private EnemyManager m_enemyManager;
        public enum GameState
        {
            Idle,
            MainMenu,
            Battle,
            Pause,
            GameOver,
            Exit,
        }

        private GameState m_curState;

        private void Start()
        {
            m_UIManager = GameManager.GetManager<UIManager>();
            m_enemyManager = GameManager.GetManager<EnemyManager>();
            m_curState = GameState.MainMenu;
            UIManager uiManager = GameManager.GetManager<UIManager>();
            m_curState = GameState.Idle;
            if(uiManager == null) 
            {
                Debug.LogError("UIManager is invalid");
                return;
            }
            uiManager.OnUIClose += this.OnUIClose;
        }

        private void OnUIClose(string uiName)
        {
            
        }

        /// <summary>
        /// A simple state machine that triggers different game actions based on the provided new game state.
        /// </summary>
        /// <param name="newState">The new state.</param>
        public void ChangeState(GameState newState)
        {
            bool bStateChanged = false;
            GameState preState = m_curState;
            if(newState == GameState.Exit)
            {
                GameManager.ExitGame();
                bStateChanged = true;
            }
            else if (m_curState == GameState.Idle && newState == GameState.MainMenu)
            {
                GameManager.EnterMenuScene();
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.MainMenu && newState == GameState.Battle)
            {
                GameManager.StartNewGame();
                m_curState = newState;
                bStateChanged = true;
            }
            else if(m_curState == GameState.Battle && newState == GameState.Pause)
            {
                GameManager.PauseGame();
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.Pause && newState == GameState.Battle)
            {
                GameManager.ResumeGame();
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.Pause && newState == GameState.MainMenu)
            {
                GameManager.Restart();
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.Battle && newState == GameState.GameOver)
            {
                GameManager.GameOver();
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.GameOver && newState == GameState.MainMenu)
            {
                GameManager.Restart();
                m_curState = newState;
                bStateChanged = true;
            }
            if (bStateChanged)
            {
                OnGameStateChange(preState, newState);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeState(GameState.Pause);
            }
        }
    }
}

