//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp
{
    public class PlayerManager : Manager
    {
        private MyAircraft m_playerAirCraft;
        [SerializeField]
        private Vector3 m_initPosition;
        private int m_playerMaxLifes;
        private int m_playerCurLifes;
        private float m_playerMaxHealth;
        private float m_playerCurHealth;

        private ResourceManager m_ResourceManager;
        private GameStateManager m_StateManager;

        public int PlayerLifes
        {
            get { return m_playerCurLifes; }
        }

        public float PlayerMaxHealth
        {
            get { return m_playerMaxHealth; }
        }
        public float PlayerCurHealth
        {
            get { return m_playerCurHealth; }
        }
        private void Start()
        {
            m_ResourceManager = GameManager.GetManager<ResourceManager>();
            m_StateManager = GameManager.GetManager<GameStateManager>();
            m_playerAirCraft = null;
            m_initPosition = new Vector3(0, 0, 0);
            m_playerMaxHealth = 100;
            m_playerCurHealth = 100;
            m_playerMaxLifes = 3;
            m_playerCurLifes = 3;
        }

        public async void SpawnPlayer()
        {
            if(m_playerAirCraft != null)
            {
                Destroy(m_playerAirCraft.gameObject);
                m_playerAirCraft = null;
            }

            GameObject playerPrefab = await m_ResourceManager.LoadPrefabAsync("PlayerShip");
            GameObject playerGo = GameObject.Instantiate(playerPrefab, transform);
            playerGo.transform.position = m_initPosition;
            playerGo.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            m_playerAirCraft = playerGo.GetComponent<MyAircraft>();
            m_playerAirCraft.Init(m_playerMaxHealth, m_playerMaxHealth, 20.0f);
            m_playerCurHealth = m_playerMaxHealth;
            m_playerAirCraft.OnHealthChange += OnPlayerHealthChange;
        }

        public void ClearPlayer()
        {
            if (m_playerAirCraft != null)
            {
                Destroy(m_playerAirCraft.gameObject);
                m_playerAirCraft = null;
            }
        }

        public void OnPlayerHealthChange(float maxHp, float curHp)
        {
            m_playerCurHealth = m_playerAirCraft.CurHP <= 0 ? 0 : m_playerAirCraft.CurHP;
            if (m_playerCurHealth == 0)
            {
                m_playerCurLifes -= 1; 
                SpawnPlayer();
            }
            if (m_playerCurLifes == 0)
            { 
                m_StateManager.ChangeState(GameStateManager.GameState.GameOver);
            }
        }

        public void ResetData()
        {
            m_playerCurLifes = 3;
        }

        // Update is called once per frame
        void Update()
        {
            if(m_playerAirCraft == null) 
            {
                return;
            }
            if (Input.GetMouseButton(0))
            {
                // First, get the mouse position in viewport, the z value is the same as the aircraft
                Vector3 mouseScreenPos = Input.mousePosition;
                Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(mouseScreenPos);
                Vector3 objPosInViewport = Camera.main.WorldToViewportPoint(transform.position);
                mouseViewportPos.z = objPosInViewport.z;
                // Then convert into world position
                Vector3 mouseWorldPos = Camera.main.ViewportToWorldPoint(mouseViewportPos);
                m_playerAirCraft.SetTargetPosition(new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f));

                m_playerAirCraft.TryFire();
            }
        }
    }
}

