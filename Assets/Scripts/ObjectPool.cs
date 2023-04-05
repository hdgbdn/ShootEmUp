//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotEmUp
{
    /// <summary>
    /// A simple object pool used to manage a large number of recurring instances.
    /// </summary>
    public class ObjectPool <T> where T : MonoBehaviour
    {
        GameObject m_prefab;
        LinkedList<GameObject> m_activedObjectList;
        Dictionary<GameObject, float> m_inactivedTimeDic;
        LinkedList<GameObject> m_inactivedObjectList;

        int m_initialSize = 10;
        float m_MaxActiveTime;

        public ObjectPool(GameObject prefab, float maxAcitveTime = 5.0f)
        {
            if(prefab.GetComponent<MonoBehaviour>() == null)
            {
                Debug.LogError("Can not create a objectpool with out attached MonoBehaviour script!");
                return;
            }    
            m_prefab = prefab;
            m_MaxActiveTime = maxAcitveTime;
            m_activedObjectList = new LinkedList<GameObject>();
            m_inactivedObjectList = new LinkedList<GameObject>();
            m_inactivedTimeDic = new Dictionary<GameObject, float>();
            for (int i = 0; i < m_initialSize; i++)
            {
                GameObject instance = CreateInstance();
                m_inactivedObjectList.AddLast(instance);
                m_inactivedTimeDic.Add(instance, Time.time);
            }
        }

        public T Acquire()
        {
            LinkedListNode<GameObject> currentNode = m_inactivedObjectList.First;
            if(currentNode != null)
            {
                m_inactivedObjectList.Remove(currentNode);
                m_inactivedTimeDic.Remove(currentNode.Value);
                m_activedObjectList.AddLast(currentNode);
                currentNode.Value.SetActive(true);
                return currentNode.Value.GetComponent<T>();
            }
            else
            {
                GameObject instance = CreateInstance();
                instance.SetActive(true);
                m_activedObjectList.AddLast(instance);
                return instance.GetComponent<T>();
            }
        }


        private GameObject CreateInstance()
        {
            GameObject instance = GameObject.Instantiate(m_prefab);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public void Clear()
        {
            foreach(var obj in m_activedObjectList) 
            {
                GameManager.Destroy(obj);
            }
            foreach (var obj in m_inactivedObjectList)
            {
                GameManager.Destroy(obj);
            }
            m_activedObjectList.Clear();
            m_inactivedObjectList.Clear();
            m_inactivedTimeDic.Clear();
        }
        public void Release(T item)
        {
            item.gameObject.SetActive(false);
            m_activedObjectList.Remove(item.gameObject);
            m_inactivedObjectList.AddLast(item.gameObject);
            if(m_inactivedTimeDic.ContainsKey(item.gameObject))
            {
                m_inactivedTimeDic[item.gameObject] = Time.time;
            }
            else
            {
                m_inactivedTimeDic.Add(item.gameObject, Time.time);
            }
            
        }

        public void DestoryInactive(T item)
        {
            m_inactivedObjectList.Remove(item.gameObject);
            m_inactivedTimeDic.Remove(item.gameObject);
            GameObject.Destroy(item.gameObject);
        }

        public void OnUpdate()
        {
            LinkedListNode<GameObject> currentNode = m_inactivedObjectList.First;
            while(currentNode != null)
            {
                float timePassed = Time.time - m_inactivedTimeDic[currentNode.Value];
                if (timePassed > m_MaxActiveTime)
                {
                    DestoryInactive(currentNode.Value.GetComponent<T>());
                }
                currentNode = currentNode.Next;
            }
        }

    }

}
