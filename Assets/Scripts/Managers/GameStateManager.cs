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
            MainMenu,
            Battle,
            GameOver,
        }

        private GameState m_curState;

        private async void Start()
        {
            m_UIManager = GameManager.GetManager<UIManager>();
            m_enemyManager = GameManager.GetManager<EnemyManager>();
            m_curState = GameState.MainMenu;
            UIManager uiManager = GameManager.GetManager<UIManager>();
            if(uiManager == null) 
            {
                Debug.LogError("UIManager is invalid");
                return;
            }
            uiManager.OnUIClose += this.OnUIClose;
            // Wait for one frame to let other manager finish their delegate attaching
            // not elegant
            await UniTask.DelayFrame(1);
            ChangeState(GameState.MainMenu);
        }

        private void OnUIClose(string uiName)
        {
            if (uiName == "UIMainMenu")
            {
                ChangeState(GameState.Battle);
            }
        }

        private void ChangeState(GameState newState)
        {
            if (newState == GameState.MainMenu)
            {
                m_curState = GameState.MainMenu;
                m_UIManager.CreateUI("UIMainMenu");

            }
            if (m_curState == GameState.MainMenu && newState == GameState.Battle)
            {
                SceneManager.LoadScene("Main");
                m_enemyManager.ShouldGenerateEnemy = true;
            }
            OnGameStateChange(newState);
        }

        private void Update()
        {

        }
    }
}

