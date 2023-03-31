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
        public LinearBulletLauncher(Aircraft craft) : base(craft) 
        {
            m_fireRate = 0.2f;
        }

        public override void TryFire()
        {

            float curTime = Time.time;
            float timePassed = curTime - m_lastfireTime;
            if(timePassed < m_fireRate)
            {
                return;
            }
            base.TryFire();
            // TODO: use entity creation here
            // now using AssetDatabase, it's an Editor only tool!
            // now using boltbullet
            GameObject bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BoltBullet.prefab");
            GameObject go = GameObject.Instantiate(bulletPrefab);
            BoltBullet bullet = go.GetComponent<BoltBullet>();
            bullet.Init(m_craft.transform.position, m_craft.transform.position + m_frontDirection * 20, 0.3f);
            m_lastfireTime = curTime;
        }
    }
}

