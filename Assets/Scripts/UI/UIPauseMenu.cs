//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotEmUp
{
    public class UIPauseMenu : MonoBehaviour
    {
        [SerializeField]
        private Button m_btnResume;
        [SerializeField]
        private Button m_btnRestart;
        [SerializeField]
        private Button m_btnExit;
        void Start()
        {
            m_btnResume.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.Battle);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });

            m_btnRestart.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.MainMenu);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });

            m_btnExit.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.Exit);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });
        }
    }
}