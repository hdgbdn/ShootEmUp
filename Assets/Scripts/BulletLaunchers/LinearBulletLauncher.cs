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
    public class LinearBulletLauncher : BulletLauncher
    {

        protected float m_fireRate;

        private BulletManager m_bulletManager;
        public LinearBulletLauncher(Aircraft craft) : base(craft) 
        {
            m_fireRate = 0.3f;
            m_bulletManager = GameManager.GetManager<BulletManager>();
        }

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
            GameObject bulletPrefab =  await m_bulletManager.AcquireBullet<BoltBullet>();
            GameObject go = GameObject.Instantiate(bulletPrefab);
            BoltBullet bullet = go.GetComponent<BoltBullet>();
            bullet.Init(m_craft.transform.position, m_craft.transform.position + m_frontDirection * 20, 0.1f);
            bullet.tag = "PlayerBullet";
            
            // Just for test: all the bullet will tagged as player's bullet

            m_lastfireTime = curTime;
        }
    }
}

