//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotEmUp
{
    public class EnemyAircraft : Aircraft
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerBullet")
            {
                m_curHP -= 40;
            }
        }

        protected override void Update()
        {
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
    }
}
