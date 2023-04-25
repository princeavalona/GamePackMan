using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PacManGame
{
    public class LevelControler : MonoBehaviour
    {
        [SerializeField] private Vector2 m_StartPosition;
        [SerializeField] private float m_StepPosition;
        [SerializeField] private Vector2 m_SizeMap;

        [SerializeField] private GameObject[] m_PrefabElement;

        [SerializeField] private EditorMap[] m_PrefabsPartMap;
        public EditorMap[] PrefabsPartMap => m_PrefabsPartMap;

        [SerializeField] private Transform m_BounderLevel;
        [SerializeField] private Transform m_PopCorn;

        private int[,] m_listLevel;
        private int m_NumberLevel;

        //[SerializeField] private CreateListLevel m_ListLevels;

        private void Start()
        {
            //m_ListLevels = new CreateListLevel();
            if (CreateListLevel.Instance.MaxLevel != 0)
            Debug.Log("YEEEEEEEES");

            

            
            m_NumberLevel = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (CreateListLevel.Instance == null)
                {
                    Debug.Log("CreateList not instance");
                }
                else
                {
                    Debug.Log("Instance Listlevel   " + CreateListLevel.Instance.ListLevel.GetLength(0));
                }
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                m_listLevel = CreateListLevel.Instance.ListLevel;

                if (m_listLevel == null)
                    Debug.Log("nooooooo");
                Debug.Log(m_listLevel.GetLength(0).ToString());
                for (int i = 0; i < m_listLevel.GetLength(0); i++)
                {
                    Debug.Log(m_listLevel[i, 0] + ", " + m_listLevel[i, 1] + ", " + m_listLevel[i, 2] + ", " + m_listLevel[i, 3]);
                }
            }

            if (Input.GetKeyUp(KeyCode.L))
            {
                DestroyChildrend();

                LoadLevel();

                m_NumberLevel++;
            }

            if (Input.GetKeyUp(KeyCode.H))
            {
                DestroyChildrend();
            }
        }


        private void LoadLevel()
        {
            //Загрузка префабов частей карт уровней            
            for (int i = 0; i < m_PrefabsPartMap.Length; i++)
            {
                if (m_PrefabsPartMap[i] == null)
                {
                    Debug.LogError("Префаб части карты " + i.ToString() + " не загружен");

                    return;
                }

                Instantiate(m_PrefabsPartMap[i], transform.position, Quaternion.identity, transform);
                
                m_PrefabsPartMap[i].Editor = false;

                if (m_PrefabsPartMap[i].MapElement == null)
                {
                    Debug.Log("Отсутствует массив элементов части карты " + i.ToString());
                }
            }
          
            for (int n = 0; n < CreateListLevel.Instance.ListLevel.GetLength(1); n++)
            {
                int sizeX = (int)m_SizeMap.x;
                int sizeY = (int)m_SizeMap.y;

                if ( n % 2 != 0)
                {
                    sizeX = (int)m_SizeMap.x - 1;
                }

                if (n > 1)
                {
                    sizeY = (int)m_SizeMap.y - 1;
                }

                int number = CreateListLevel.Instance.ListLevel[m_NumberLevel, n];

                for (int j = 0; j < sizeY; j++)
                {
                    for (int i = 0; i < sizeX; i++)
                    {
                        int k = j * (int)m_SizeMap.x + i;                        

                        if (m_PrefabsPartMap[number].MapElement[k].x >= m_PrefabElement.Length)
                        {
                            Debug.Log("Префаб элемента уровня не подгружен в массив");
                            return;
                        }

                        if (m_PrefabsPartMap[number].MapElement[k].x >= 0)
                        {
                            var element = Instantiate(m_PrefabElement[(int)m_PrefabsPartMap[number].MapElement[k].x], transform);

                            PrefabMap type = element.GetComponent<PrefabMap>();

                            element.transform.position = PositionElementMap(n, i, j);

                            element.transform.eulerAngles = RotationElement(n, (int)m_PrefabsPartMap[number].MapElement[k].y, type.Symmetry);
                        }
                    }
                }
            }
        }

        private Vector3 PositionElementMap(int part, int i, int j)
        {
            if (part == 0)
            {
                return new Vector3(m_StartPosition.x + m_StepPosition * i, m_StartPosition.y - m_StepPosition * j, 0);
            }

            if (part == 1)
            {
                return new Vector3(m_StartPosition.x + (m_SizeMap.x - 1) * m_StepPosition * 2 - m_StepPosition * i,
                                  m_StartPosition.y - m_StepPosition * j, 0);
            }

            if (part == 2)
            {
                return new Vector3(m_StartPosition.x + m_StepPosition * i, -m_StartPosition.y + m_StepPosition * j, 0);
            }

            return new Vector3(m_StartPosition.x + (m_SizeMap.x - 1) * m_StepPosition * 2 - m_StepPosition * i,
                                  -m_StartPosition.y + m_StepPosition * j, 0);
        }

        private Vector3 RotationElement(int part, int view, TypeSymmetry type)
        {
            if (type == TypeSymmetry.AsymmetryType)
            {
                if ((view % 2 == 0 && part == 1) || (view % 2 != 0 && part == 2))
                {
                    return new Vector3(0, 0, 90 * RotationIndex(view + 1));
                }

                if ((view % 2 != 0 && part == 1) || (view % 2 == 0 && part == 2))
                {
                    return new Vector3(0, 0, 90 * RotationIndex(view - 1));
                }

                if (part == 3)
                {
                    return new Vector3(0, 0, 90 * RotationIndex(view + 2));
                }
            }

            if (type == TypeSymmetry.SymmetryType)
            {
                if ((view % 2 == 0 && part % 2 != 0) || (view % 2 != 0 && part > 1))
                {
                    return new Vector3(0, 0, 90 * RotationIndex(view + 2));
                }
            }

            return new Vector3(0, 0, 90 * view);
        }

        private int RotationIndex(int view)
        {
            if (view > 3) return view - 4;
            if (view < 0) return view + 4;

            return view;
        }

        private void DestroyChildrend()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

    }
}