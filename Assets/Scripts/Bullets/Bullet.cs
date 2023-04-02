using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Vector3 m_startPosition;
    protected Vector3 m_endPosition;

    public virtual void Init(Vector3 startPos, Vector3 endPos)
    {
        m_startPosition = startPos;
        m_endPosition = endPos;
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

    protected virtual void DestorySelf()
    {
        GameObject.Destroy(gameObject);
    }
}
