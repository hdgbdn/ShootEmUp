using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp 
{
    public class Aircraft : MonoBehaviour
    {
        protected Vector3 m_TargetPosition;
        protected float m_Speed = 12.0f;
        protected List<BulletLauncher> m_launchers;
        protected virtual void Start()
        {
            m_launchers = new List<BulletLauncher>();
        }

        public void AttachWepon(BulletLauncher newLauncher)
        {
            m_launchers.Add(newLauncher);
        }

        protected virtual void Update()
        {
            
        }
    }
}

