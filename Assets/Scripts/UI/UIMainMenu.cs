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
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button btn_StartGame;

        private void Start()
        {
            btn_StartGame.onClick.AddListener(() =>
            {
                GameManager.GameState.ChangeState(GameStateManager.GameState.Battle);
                GameManager.UI.DestroyUI("UIMainMenu"); 
            });
        }

      
    }
}

