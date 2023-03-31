using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotEmUp
{
    public class MyAircraft : Aircraft
    {
        protected override void Start()
        {
            base.Start();
            m_launchers.Add(new LinearBulletLauncher(this));
        }
        protected override void Update()
        {
            base.Update();
            // Make the position where mouse clicked the new target position
            // And fire weapons
            if (Input.GetMouseButton(0))
            {
                // First, get the mouse position in viewport, the z value is the same as the aircraft
                Vector3 mouseScreenPos = Input.mousePosition;
                Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(mouseScreenPos);
                Vector3 objPosInViewport = Camera.main.WorldToViewportPoint(transform.position);
                mouseViewportPos.z = objPosInViewport.z;
                // Then convert into world position
                Vector3 mouseWorldPos = Camera.main.ViewportToWorldPoint(mouseViewportPos);
                m_TargetPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f);

                // Fire all weapons
                for (int i = 0; i < m_launchers.Count; i++)
                {
                    m_launchers[i].TryFire();
                }
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
}

