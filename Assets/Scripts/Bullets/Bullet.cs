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
    public class Bullet : MonoBehaviour
    {
        protected Vector3 m_startPosition;
        protected Vector3 m_endPosition;
        protected BulletLauncher m_Launcher;

        public virtual void Init(Vector3 startPos, Vector3 endPos, BulletLauncher launcher)
        {
            m_startPosition = startPos;
            m_endPosition = endPos;
            transform.position = m_startPosition;
            m_Launcher = launcher;
        }

        protected virtual void Start()
        {
            transform.position = m_startPosition;
        }

        protected virtual void Update()
        {

        }

        protected virtual void OnTriggerEnter(Collider other)
        {

        }

        protected virtual void HideSelf()
        {
            m_Launcher.HideBullet(this);
        }
    }
}

