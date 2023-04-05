//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp 
{
    public class Aircraft : MonoBehaviour
    {
        protected float m_maxHP;
        protected float m_curHP;
        protected float m_Speed;
        protected Vector3 m_TargetPosition;
        protected List<BulletLauncher> m_launchers;

        public virtual void Init(float maxHp, float curHp, float speed = 12.0f)
        {
            m_maxHP = maxHp;
            m_curHP = curHp;
            m_Speed = speed;
        }
        protected virtual void Start()
        {
            m_launchers = new List<BulletLauncher>();
        }

        public void AttachWepon(BulletLauncher newLauncher)
        {
            m_launchers.Add(newLauncher);
        }

        public void SetTargetPosition(Vector3 newPosition)
        {
            m_TargetPosition = newPosition;
        }

        public void TryFire()
        {
            for (int i = 0; i < m_launchers.Count; i++)
            {
                m_launchers[i].TryFire();
            }
        }
    }
}

