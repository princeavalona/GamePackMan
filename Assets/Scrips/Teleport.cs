using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacManGame
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Teleport m_OtherBounder;

        private PacMan m_PacMan;
        private bool m_IsActive;
        public bool IsActive
        {
            set { m_IsActive = value; }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            m_PacMan = collision.transform.GetComponent<PacMan>();


            if (m_PacMan != null && m_IsActive == false)
            {
                Rigidbody2D m_RigidBodyPacMan = m_PacMan.GetComponent<Rigidbody2D>();

                if (m_RigidBodyPacMan != null)
                {
                    if (m_RigidBodyPacMan.velocity.y != 0)
                    {
                        m_PacMan.transform.position = new Vector3(m_PacMan.transform.position.x, m_OtherBounder.transform.position.y, 0);
                    }

                    if (m_RigidBodyPacMan.velocity.x != 0)
                    {
                        m_PacMan.transform.position = new Vector3(m_OtherBounder.transform.position.x, m_PacMan.transform.position.y, 0);
                    }
                }

                m_OtherBounder.IsActive = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            MovementController movement = m_PacMan.transform.GetComponent<MovementController>();

            if (movement != null)
            {
                if (movement.IsMoving == true)
                {
                    movement.IsMoving = false;
                }
                else
                {
                    movement.IsMoving = true;
                }
            }

            m_IsActive = false;
        }
    }
}