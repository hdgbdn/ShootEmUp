//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ShotEmUp
{
    /// <summary>
    /// A bullet launcher that launche bullet in a line.
    /// </summary>
    public class LinearBulletLauncher : BulletLauncher
    {

        protected float m_fireRate;

        protected float m_bulletSpeed;
        protected float m_bulletRange;

        public LinearBulletLauncher(Aircraft craft) : base(craft) 
        {
            m_fireRate = 0.3f;
            m_bulletSpeed = 20f;
            m_bulletRange = 20f;
        }

        /// <summary>
        /// Try to launch bullets, make it moves in a line since it's a linear launcher.
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

            // Now using boltbullet
            Bullet bullet = await m_bulletManager.AcquireBullet<BoltBullet>();
            BoltBullet boltBullet =  bullet as BoltBullet;
            if (boltBullet.gameObject == null)
            {
                Debug.LogError(string.Format("Failed to acquire a {0} from the BulletManager", typeof(BoltBullet).Name));
                return;
            }
            boltBullet.Init(m_craft.transform.position, m_craft.transform.position + m_frontDirection * m_bulletRange, m_bulletSpeed, this);

            // Just for test: all the bullet will tagged as player's bullet
            bullet.tag = "PlayerBullet";
            m_lastfireTime = curTime;
        }

    }
}

