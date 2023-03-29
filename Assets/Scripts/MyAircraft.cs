using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAircraft : MonoBehaviour
{
    private Vector3 m_TargetPosition;
    private float m_Speed = 12.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_TargetPosition = new Vector3(point.x, point.y, 0f);
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
}
