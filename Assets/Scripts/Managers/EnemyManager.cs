//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

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

        ObjectPool<EnemyAircraft> m_enemyPool;

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

        public void ClearAllEnemies()
        {
            if(m_enemyPool != null)
            {
                m_enemyPool.Clear();
            }
            
        }
        
        public void HideEnemy(EnemyAircraft enemy)
        {
            if(m_enemyPool != null)
            {
                m_enemyPool.Release(enemy);
            }
        }

        public void OnEnemyHealthChange(EnemyAircraft enemy, float cur_Hp)
        {
            if(enemy != null)
            {
                if (cur_Hp <= 0)
                {
                    HideEnemy(enemy);
                }
            }
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
            float randomPositionX = m_enemyGenerateBoundary.bounds.min.x + m_enemyGenerateBoundary.bounds.size.x * UnityEngine.Random.value;

            // Using lazy initialization in update.
            // If asynchronous resource loading operations are performed in the Start function,
            // it is possible that the Update function may be called before Start is finished.
            if (m_enemyPool == null)
            {
                GameObject enemyPrefab = await m_ResourceManager.LoadPrefabAsync("EnemyShip");
                m_enemyPool = new ObjectPool<EnemyAircraft>(enemyPrefab);
            }
            EnemyAircraft enemy = m_enemyPool.Acquire();
            Vector3 startPos = new Vector3(randomPositionX, m_enemyGenerateBoundary.bounds.center.y, 0);
            Vector3 endPos = startPos + new Vector3(0, -200, 0);
            enemy.transform.position = startPos;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 180);
            enemy.Init(100, 100, 5.0f);
            enemy.SetTargetPosition(endPos);
            m_lastGenerateTime = curTime;

            m_enemyPool.OnUpdate();
        }
    }
}

