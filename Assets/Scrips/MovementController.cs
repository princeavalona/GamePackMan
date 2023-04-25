using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacManGame
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float m_Speed;

        private Rigidbody2D m_RigidBodyPacMan;
        private Vector2 m_MoveVector;

        private bool m_IsMoving;
        public bool IsMoving
        {
            get { return m_IsMoving; }
            set { m_IsMoving = value; }
        }


        private void Awake()
        {
            m_RigidBodyPacMan = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            m_IsMoving = true;
        }

        private void Update()
        {
            if (m_IsMoving == false) { Debug.Log("stop"); }
            if (m_IsMoving == true)
            {
                Movement();
            }
        }

        private void Movement()
        {
            m_MoveVector.x = Input.GetAxisRaw("Horizontal");
            m_MoveVector.y = Input.GetAxisRaw("Vertical");

            if (m_MoveVector.x != 0)
            {
                m_RigidBodyPacMan.velocity = new Vector2(m_MoveVector.x * m_Speed, 0);
            }

            if (m_MoveVector.y != 0)
            {
                m_RigidBodyPacMan.velocity = new Vector2(0, m_MoveVector.y * m_Speed);
            }
        }
    }
}