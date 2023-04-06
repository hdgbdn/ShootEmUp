//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotEmUp
{
    /// <summary>
    /// A simple UI manager for creating and destroying UI elements.
    /// </summary>
    public class UIManager : Manager
    {
        private Transform m_uiRoot;
        private static string s_uiPathPrefix = "Assets/UI/{0}.prefab";
        private LinkedList<GameObject> m_openedUIList;

        public delegate void OnUICloseDelegate(string uiName);

        public event OnUICloseDelegate OnUIClose;
        public ResourceManager resourceManager;

        private void Start()
        {
            m_openedUIList = new LinkedList<GameObject>();
            m_uiRoot = transform;


            GameStateManager gameStateManager = GameManager.GetManager<GameStateManager>();
            if (gameStateManager == null)
            {
                Debug.LogError("GameStateManager is invalid");
                return;
            }
            gameStateManager.OnGameStateChange += OnGameStateChange;

            resourceManager = GameManager.GetManager<ResourceManager>();
        }
        async public void CreateUI(string uiName)
        {
            GameObject uiPrefab = await resourceManager.LoadUIAsync(uiName);
            GameObject uiGo = GameObject.Instantiate(uiPrefab, m_uiRoot);
            uiGo.name = uiName;
            m_openedUIList.AddLast(uiGo);
        }

        public void DestroyUI(string uiName)
        {
            LinkedListNode<GameObject> curNode = m_openedUIList.First;
            while (curNode != null)
            {
                if (curNode.Value.name == uiName)
                {
                    GameObject.Destroy(curNode.Value);
                    m_openedUIList.Remove(curNode);
                    OnUIClose(uiName);
                }
                curNode = curNode.Next;
            }
            return;
        }

        private void OnGameStateChange(GameStateManager.GameState preState, GameStateManager.GameState newState)
        {
          if(newState == GameStateManager.GameState.MainMenu || newState == GameStateManager.GameState.Idle) 
            {
                DestroyUI("UIHealthBar");
            }
        }
    }

}
