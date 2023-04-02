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
    public class ResourceManager : Manager
    {

        private static string s_uiPathPrefix = "UI/{0}";
        private static string s_prefabPathPrefix = "Prefabs/{0}";

        public async UniTask<GameObject> LoadUIAsync(string uiName)
        {
            string path = string.Format(s_uiPathPrefix, uiName);
            GameObject go = await LoadInternal<GameObject>(path);
            go.name = uiName;
            return go;
        }

        public async UniTask<GameObject> LoadPrefabAsync(string prefabName)
        {
            string path = string.Format(s_prefabPathPrefix, prefabName);
            GameObject go = await LoadInternal<GameObject>(path);
            go.name = prefabName;
            return go;
        }

        private async UniTask<T> LoadInternal<T>(string path) where T : UnityEngine.Object
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

