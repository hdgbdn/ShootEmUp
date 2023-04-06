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
    /// <summary>
    /// Player's aircraft.
    /// </summary>
    public class MyAircraft : Aircraft
    {
        public event AircraftHealthChangeDelegate OnHealthChange;
        protected override void Start()
        {
            base.Start();
            m_launchers.Add(new LinearBulletLauncher(this));

        }

        public override void Init(float maxHp, float curHp, float speed = 12.0f)
        {
            base.Init(maxHp, curHp, speed);
        }

        protected void Update()
        {
            // The m_TargetPosition is controlled by it's manager.
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

        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EnemyAircraft" || other.tag == "EnemyBullet")
            {
                m_curHP -= 20;
                OnHealthChange(m_maxHP, m_curHP);
            }
            if (m_curHP <= 0)
            {
                m_curHP = 0;
            }
        }
    }
}

