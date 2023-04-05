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
        private Vector3 m_initPosition = new Vector3(0, 0, 0);

        private ResourceManager m_ResourceManager;

        private void Start()
        {
            m_ResourceManager = GameManager.GetManager<ResourceManager>();
            m_playerAirCraft = null;
        }

        public async void SwapPlayer()
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
            m_playerAirCraft.Init(100, 100, 20.0f);
        }

        public void ClearPlayer()
        {
            if (m_playerAirCraft != null)
            {
                Destroy(m_playerAirCraft.gameObject);
                m_playerAirCraft = null;
            }
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

