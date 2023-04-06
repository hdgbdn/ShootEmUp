//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace ShotEmUp
{
    /// <summary>
    /// A manager arrange all the bullets, include player's and enemy's bullets.
    /// Using objectpool to reduce the GC of Bullet GameObjects.
    /// </summary>
    public class BulletManager : Manager
    {
        private ResourceManager m_resourceManager;

        // Using Type GameObject pairs to cache bullet prefab
        private Dictionary<Type, GameObject> m_typeBulletPairs;

        // A object pool to manage all types of bullets
        private Dictionary<Type, ObjectPool<Bullet>> m_bulletPools;

        private void Start()
        {
            m_resourceManager = GameManager.GetManager<ResourceManager>();
            m_typeBulletPairs = new Dictionary<Type, GameObject>();

            m_bulletPools= new Dictionary<Type, ObjectPool<Bullet>>();
        }

        /// <summary>
        /// Acquire a instance of a certain type bullet.
        /// </summary>
        /// <typeparam name="T">The bullet type.</typeparam>
        /// <returns></returns>
        public async UniTask<Bullet> AcquireBullet<T>() where T : Bullet
        {
            Type bulletType = typeof(T);
            // If we don't have the certain type of object pool, create one
            if (!m_bulletPools.ContainsKey(bulletType))
            {
                var bulletPrefab = await m_resourceManager.LoadBulletAsync<T>();
                var newObjPool = new ObjectPool<Bullet>(bulletPrefab);
                m_bulletPools.Add(bulletType, newObjPool);
            }
            Bullet bullet = m_bulletPools[bulletType].Acquire();
            if(bullet == null) 
            {
                Debug.LogWarning(string.Format("Failed to load {0} from the BulletManager", bulletType.Name));
                return null;
            }
            return bullet;
        }

        /// <summary>
        /// Check object pool to release object by time passed.
        /// </summary>
        private void Update()
        {
            foreach(var poolNode in m_bulletPools)
            {
                poolNode.Value.OnUpdate();
            }
        }

        public void ClearAllBullets()
        {
            m_bulletPools.Clear();
        }

        public void OnHideBullet(Bullet bullet)
        {
            var pool = InternalGetPoolByBulletType(bullet.GetType());
            pool.Release(bullet);
        }

        private ObjectPool<Bullet> InternalGetPoolByBulletType(Type typ)
        {
            if (!m_bulletPools.ContainsKey(typ))
            {
                Debug.LogError(string.Format("The manager's pool don't contain a type {0}", typ.Name));
                return null;
            }
            else
            {
                return m_bulletPools[typ];
            }

        }
    }
}

