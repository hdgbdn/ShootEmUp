using ShotEmUp;
//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp
{
    public class Manager : MonoBehaviour
    {
        protected void Awake()
        {
            GameManager.RegisterManager(this);
        }
    }
}

