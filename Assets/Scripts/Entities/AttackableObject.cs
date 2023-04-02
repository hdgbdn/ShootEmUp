//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The common base class of all attackable objects
/// </summary>
public class AttackableObject : MonoBehaviour
{
    protected float m_maxHP;
    protected float m_curHP;
    protected float m_speed;

    public virtual void Init(float maxHp, float curHp, float speed)
    {
        m_maxHP = maxHp;
        m_curHP = curHp;
        m_speed = speed;
    }

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = transform.position + Time.deltaTime * m_speed * transform.up;
        if(m_curHP <= 0) Destroy(gameObject);
    }
}
