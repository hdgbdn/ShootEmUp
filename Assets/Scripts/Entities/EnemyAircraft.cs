//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp
{
    public class EnemyAircraft : Aircraft
    {
        protected EnemyManager m_enmyManager;
        protected float m_initTime;
        protected float m_lifeTime = 20f;

        public override void Init(float maxHp, float curHp, float speed = 12.0f)
        {
            base.Init(maxHp, curHp, speed);
            m_enmyManager = GameManager.Enemy;
            m_initTime = Time.time;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerBullet")
            {
                m_curHP -= 40;
            }
            if(m_curHP <= 0) 
            { 
                HideSelf();
            }
        }

        protected void Update()
        {
            if (Time.time - m_initTime >= m_lifeTime)
            {
                HideSelf();
                return;
            }
            Vector3 direction = m_TargetPosition - transform.position;
            if (direction.sqrMagnitude <= Vector3.kEpsilon)
            {
                return;
            }

            Vector3 moveVector = Vector3.ClampMagnitude(direction.normalized * m_Speed * Time.deltaTime, direction.magnitude);
            transform.position = new Vector3
            (
                transform.position.x + moveVector.x,
                transform.position.y + moveVector.y,
                0f
            );
        }

        protected void HideSelf()
        {
            m_enmyManager.HideEnemy(this);
        }
    }
}
