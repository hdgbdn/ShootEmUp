﻿//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------

using ShotEmUp;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ShotEmUp 
{
    public class BulletLauncher
    {
        protected Bullet m_bullet;
        protected Aircraft m_craft;
        protected Vector3 m_frontDirection;
        protected Vector3 m_rightDirection;
        protected float m_lastfireTime;

        public BulletLauncher(Aircraft craft)
        {
            m_craft = craft;
            m_frontDirection = craft.transform.up;
            m_rightDirection = craft.transform.right;
            m_lastfireTime = 0.0f;
        }
        public virtual void TryFire()
        {

        }
    }
}

