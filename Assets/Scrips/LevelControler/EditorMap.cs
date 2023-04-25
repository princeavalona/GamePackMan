using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacManGame
{
    public class EditorMap : MonoBehaviour
    {
        [SerializeField] private bool m_Editor;
        public bool Editor { get; set; }

        [SerializeField] private Vector2 m_StartPosition;
        [SerializeField] private float m_StepPosition;
        [SerializeField] private Vector2 m_SizeMap;
        [SerializeField] private Vector2[] m_MapElement;
        public Vector2[] MapElement => m_MapElement;

        [SerializeField] private GameObject[] m_PrefabElement;
        public GameObject[] PrefabElement => m_PrefabElement;


        void Start()
        {
            if (m_Editor == true)
            {
                CreateMap();
            }
        }

        private void CreateMap()
        {
            for (int i = 0; i < m_PrefabElement.Length; i++)
            {
                if (m_PrefabElement[i] == null)
                {
                    Debug.Log("Нет префабов элементов уровня");
                    return;
                }
            }

            if (m_MapElement.Length < m_SizeMap.x * m_SizeMap.y)
            {
                Debug.Log("Количество элементов меньше размера карты уровня");
                return;
            }

            for (int i = 0; i < m_SizeMap.y; i++)
            {
                for (int j = 0; j < m_SizeMap.x; j++)
                {
                    int k = i * (int)m_SizeMap.x + j;

                    if (m_MapElement[k].x >= m_PrefabElement.Length)
                    {
                        Debug.Log("Префаб элемента уровня не подгружен в массив");
                        return;
                    }
                    Vector3 pos = new Vector3(m_StartPosition.x + m_StepPosition * j, m_StartPosition.y - m_StepPosition * i, 0);

                    Quaternion quat = new Quaternion();// (0, 0, 90, 0);
                    quat.eulerAngles = new Vector3(0, 0, 90 * m_MapElement[k].y);

                    if (m_MapElement[k].x >= 0)
                    {
                        Instantiate(m_PrefabElement[(int)m_MapElement[k].x], pos, quat, transform);
                    }
                }
            }
        }
    }
}