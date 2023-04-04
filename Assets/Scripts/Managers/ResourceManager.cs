//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Xml.Linq;

namespace ShotEmUp
{
    /// <summary>
    /// A resource load manager.
    /// As an small project, I use Unity.Resources to load resources.
    /// And thanks to UniTask, I can write clean asynchronous code without lots of callbacks.
    /// </summary>
    public class ResourceManager : Manager
    {

        private static string s_uiPathPrefix = "UI/{0}";
        private static string s_bulletPathPrefix = "Prefabs/{0}";
        private static string s_prefabPathPrefix = "Prefabs/{0}";

        /// <summary>
        /// Load UI Prefab Async.
        /// </summary>
        /// <param name="uiName">The UI prefab's name without extention</param>
        /// <returns></returns>
        public async UniTask<GameObject> LoadUIAsync(string uiName)
        {
            string path = string.Format(s_uiPathPrefix, uiName);
            GameObject go = await InteralLoad<GameObject>(path);
            if(go != null) go.name = uiName;
            return go;
        }

        /// <summary>
        /// Load Common prefab Async.
        /// </summary>
        /// <param name="bulletName">The prefab's name without extention</param>
        /// <returns></returns>
        public async UniTask<GameObject> LoadBulletAsync<T>()
        {
            string bulletName = typeof(T).Name;
            string path = string.Format(s_bulletPathPrefix, typeof(T).Name);
            GameObject go = await InteralLoad<GameObject>(path);
            if (go != null) go.name = bulletName;
            return go;
        }

        /// <summary>
        /// Load Common prefab Async.
        /// </summary>
        /// <param name="prefabName">The prefab's name without extention</param>
        /// <returns></returns>
        public async UniTask<GameObject> LoadPrefabAsync(string prefabName)
        {
            string path = string.Format(s_prefabPathPrefix, prefabName);
            GameObject go = await InteralLoad<GameObject>(path);
            if (go != null) go.name = prefabName;
            return go;
        }

        /// <summary>
        /// Generics internal load resource function.
        /// </summary>
        /// <typeparam name="T">The type of resource.</typeparam>
        /// <param name="path">Path of the resource. (The relative path in the 'Assets/Resources' directory, such as 'Prefabs/Aircraft'</param>
        /// <returns></returns>
        private async UniTask<T> InteralLoad<T>(string path) where T : UnityEngine.Object
        {
            var asyncOperation = Resources.LoadAsync(path, typeof(T));
            await asyncOperation;
            T resource = asyncOperation.asset as T;
            if (resource == null)
            {
                Debug.LogWarning(string.Format("Asset: {0} load failed!", path));
            }
            return resource;
        }    
    }
}

