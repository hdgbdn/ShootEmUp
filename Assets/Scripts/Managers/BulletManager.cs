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
using System;

namespace ShotEmUp
{
    /// <summary>
    /// A manager arrange all the bullets, include player's and enemy's bullets.
    /// Using objectpool to enduce the GC of GameObjects.
    /// </summary>
    public class BulletManager : Manager
    {
        private ResourceManager m_resourceManager;

        // Using Type GameObject pairs to cache bullet prefab
        private Dictionary<Type, GameObject> m_typeBulletPairs;

        private void Start()
        {
            m_resourceManager = GameManager.GetManager<ResourceManager>();
            m_typeBulletPairs = new Dictionary<Type, GameObject>();
        }
        public async UniTask<GameObject> AcquireBullet<T>()
        {
            GameObject bulletPrefab;
            if (m_typeBulletPairs.TryGetValue(typeof(T), out bulletPrefab) && bulletPrefab != null)
            {
                return bulletPrefab;
            }
            else
            {
                bulletPrefab = await m_resourceManager.LoadPrefabAsync("BoltBullet");
                return bulletPrefab;
            }
        }
    }
}

