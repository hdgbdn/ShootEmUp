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

        public delegate void OnGameStateChangeDelegate(GameState newState);

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

        private async void Start()
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

        public void ChangeState(GameState newState)
        {
            bool bStateChanged = false;
            if(newState == GameState.Exit)
            {
                bStateChanged = true;
            }
            else if (m_curState == GameState.Idle && newState == GameState.MainMenu)
            {
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.MainMenu && newState == GameState.Battle)
            {
                m_curState = newState;
                bStateChanged = true;
            }
            else if(m_curState == GameState.Battle && newState == GameState.Pause)
            {
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.Pause && newState == GameState.Battle)
            {
                m_curState = newState;
                bStateChanged = true;
            }
            else if (m_curState == GameState.Pause && newState == GameState.MainMenu)
            {
                m_curState = newState;
                bStateChanged = true;
            }
            if (bStateChanged)
            {
                OnGameStateChange(newState);
            }
        }

        private void Update()
        {

        }
    }
}

