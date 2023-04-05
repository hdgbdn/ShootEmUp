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
        private Button btn_Resume;
        [SerializeField]
        private Button btn_Restart;
        [SerializeField]
        private Button btn_Exit;
        void Start()
        {
            btn_Resume.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.Battle);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });

            btn_Restart.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.MainMenu);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });

            btn_Exit.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.Exit);
                GameManager.UI.DestroyUI("UIPauseMenu");
            });
        }
    }
}