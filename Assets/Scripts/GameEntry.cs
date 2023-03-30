//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp
{
    public class GameEntry : MonoBehaviour
    {
        private static readonly LinkedList<Manager> s_Managets = new LinkedList<Manager>();

        public static void RegisterManager(Manager manager)
        {
            if (manager == null)
            {
                Debug.LogError("Manager is invalid.");
                return;
            }

            Type type = manager.GetType();

            LinkedListNode<Manager> curNode = s_Managets.First;

            while (curNode != null)
            {
                if (curNode.Value.GetType() == type)
                {
                    Debug.LogError(string.Format("Manager type '{0}' is already exist.", type.FullName));
                    return;
                }
                curNode = curNode.Next;
            }

            s_Managets.AddLast(manager);
        }

        public static T GetManager<T>() where T : Manager
        {
            Type typ = typeof(T);

            LinkedListNode<Manager> curNode = s_Managets.First;

            while(curNode != null)
            {
                if (curNode.Value.GetType() == typ)
                {
                    return curNode.Value as T;
                }
                curNode = curNode.Next;
            }

            return null;
        }

        private void Start()
        {
            
        }
        

    }
}

