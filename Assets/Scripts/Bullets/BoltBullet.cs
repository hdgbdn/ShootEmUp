//------------------------------------------------------------
// ShootEmUp
// Copyright © 2013-2021 Hu Di. All rights reserved.
// E-mail: mailto:hdgbdn92@gmail.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBullet : Bullet
{
    protected float m_speed;
    protected float m_length;

    public virtual void Init(Vector3 startPos, Vector3 endPos, float speed)
    {
        base.Init(startPos, endPos);
        m_speed = speed;
        m_length = (m_endPosition - m_startPosition).magnitude;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (gameObject.tag == "PlayerBullet" && other.tag == "Attackable") 
        {
            DestorySelf();
            return;
        }
        if (gameObject.tag == "EnemyBullet" && other.tag == "Player")
        {
            DestorySelf();
            return;
        }
    }

    protected override void Update()
    {
        base.Update();
        Vector3 direction = (m_endPosition - m_startPosition).normalized;
        Vector3 targetPosition = transform.position + direction * m_speed;
        transform.position = targetPosition;
        if((targetPosition - m_startPosition).magnitude > m_length)
        {
            DestorySelf();
        }
    }
}
