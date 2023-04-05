//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotEmUp
{
    public class EnemyManager : Manager
    {
        [SerializeField]
        private float m_generateRate = 3.0f;

        [SerializeField]
        private Collider m_enemyGenerateBoundary;

        [SerializeField]
        private BoxCollider m_VisibleBoundary;

        [SerializeField]
        private BoxCollider m_PlayerMoveBoundary;

        private float m_lastGenerateTime;
        private bool m_shouldGenerate;

        private ResourceManager m_ResourceManager;


        void Start()
        {
            m_lastGenerateTime = Time.time;
            m_shouldGenerate = false;

            m_ResourceManager = GameManager.GetManager<ResourceManager>();
        }

        public void StartGenerateEnemy()
        {
            m_shouldGenerate = true;
        }

        public void StopGenerateEnemy()
        {
            m_shouldGenerate = false;
        }

        async void Update()
        {
            if (!m_shouldGenerate) 
            {
                return;
            }
            float curTime = Time.time;
            float timePassed = curTime - m_lastGenerateTime;
            if (timePassed < m_generateRate)
            {
                return;
            }
            float randomPositionX = m_enemyGenerateBoundary.bounds.min.x + m_enemyGenerateBoundary.bounds.size.x * Random.value;

            GameObject enemyPrefab = await m_ResourceManager.LoadPrefabAsync("EnemyShip");
            GameObject go = GameObject.Instantiate(enemyPrefab, transform);
            go.transform.position = new Vector3(randomPositionX, m_enemyGenerateBoundary.bounds.center.y, 0);
            go.transform.rotation = Quaternion.Euler(0, 0, 180);
            EnemyAircraft enemy = go.GetComponent<EnemyAircraft>();
            enemy.Init(100, 100, 5.0f);

            m_lastGenerateTime = curTime;
        }
    }
}

