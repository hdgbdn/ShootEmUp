using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAircraft : AttackableObject
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet") 
        {
            m_curHP -= 40;
        }
    }
}
