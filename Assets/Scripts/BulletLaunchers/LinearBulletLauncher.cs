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
    /// <summary>
    /// A bullet launcher that launche bullet in a line.
    /// </summary>
    public class LinearBulletLauncher : BulletLauncher
    {

        protected float m_fireRate;

        private BulletManager m_bulletManager;
        public LinearBulletLauncher(Aircraft craft) : base(craft) 
        {
            m_fireRate = 0.3f;
            m_bulletManager = GameManager.GetManager<BulletManager>();
        }

        /// <summary>
        /// Try to launch bullets, make it moves in a line.
        /// </summary>
        public async override void TryFire()
        {

            float curTime = Time.time;
            float timePassed = curTime - m_lastfireTime;
            if(timePassed < m_fireRate)
            {
                return;
            }
            base.TryFire();

            // now using boltbullet
            GameObject go = await m_bulletManager.AcquireBullet<BoltBullet>();
            if(go == null)
            {
                Debug.LogError(string.Format("Failed to acquire a {0} from the BulletManager", typeof(BoltBullet).Name));
                return;
            }
            BoltBullet bullet = go.GetComponent<BoltBullet>();
            bullet.Init(m_craft.transform.position, m_craft.transform.position + m_frontDirection * 20, 0.1f);

            // Just for test: all the bullet will tagged as player's bullet
            bullet.tag = "PlayerBullet";
            m_lastfireTime = curTime;
        }
    }
}

