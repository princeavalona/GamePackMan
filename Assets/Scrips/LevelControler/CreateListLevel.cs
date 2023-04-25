using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacManGame
{
    public class CreateListLevel : SingletonBase<CreateListLevel>
    {
        private const int c_PartMap = 4;

        [SerializeField] private LevelControler m_AmountPartMap;

        private int m_NumberLevel;

        private bool m_FullSymmetry;
        private bool m_DoubleUpSymmetry;
        private bool m_DoubleDownSymmetry;
        private bool m_SlantSymmetry;
        private bool m_AsymmetryUp;
        private bool m_AssymetryDown;
        private bool m_AssymetryNext;

        private float m_MaxLevel;
        public float MaxLevel => m_MaxLevel;

        private int[,] m_listLevel;
        public int[,] ListLevel => m_listLevel;

        void Start()
        {
            m_FullSymmetry = false;
            m_DoubleUpSymmetry = false;
            m_DoubleDownSymmetry = false;
            m_SlantSymmetry = false;
            m_AsymmetryUp = false;
            m_AssymetryDown = false;
            m_AssymetryNext = false;

            m_NumberLevel = m_AmountPartMap.PrefabsPartMap.Length;
            
            m_MaxLevel = Mathf.Pow((float)m_NumberLevel, (float)c_PartMap);
            
            m_listLevel = new int[(int)m_MaxLevel, c_PartMap];
            
            GenerativeListLevel();
        }

        private void GenerativeListLevel()
        {
            int i = 0;

            while (i < m_MaxLevel)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;

                while (m_FullSymmetry == false)
                {
                    for (int n = 0; n < c_PartMap; n++)
                    {
                        m_listLevel[i, n] = a;
                    }

                    i++;
                    a++;

                    if (a == m_NumberLevel)
                    {
                        m_FullSymmetry = true;
                        a = 0;
                    }
                }

                while (m_DoubleUpSymmetry == false)
                {
                    if (b == a)
                    {
                        b++;
                    }

                    if (b == m_NumberLevel)
                    {
                        a++;
                        b = 0;

                        if (a == m_NumberLevel)
                        {
                            a = 0;
                            b = 0;

                            m_DoubleUpSymmetry = true;

                            break;
                        }
                    }

                    for (int n = 0; n < c_PartMap; n++)
                    {
                        if (n < c_PartMap / 2)
                        {
                            m_listLevel[i, n] = a;
                        }
                        else
                        {
                            m_listLevel[i, n] = b;
                        }
                    }

                    i++;
                    b++;
                }

                while (m_DoubleDownSymmetry == false)
                {
                    if (b == a)
                    {
                        b++;
                    }

                    if (b == m_NumberLevel)
                    {
                        a++;
                        b = 0;

                        if (a == m_NumberLevel)
                        {
                            a = 0;
                            b = 0;

                            m_DoubleDownSymmetry = true;

                            break;
                        }
                    }

                    for (int n = 0; n < c_PartMap; n++)
                    {
                        if (n % 2 == 0)
                        {
                            m_listLevel[i, n] = a;
                        }
                        else
                        {
                            m_listLevel[i, n] = b;
                        }
                    }

                    i++;
                    b++;
                }

                while (m_SlantSymmetry == false)
                {
                    if (b == a)
                    {
                        b++;
                    }

                    if (b == m_NumberLevel)
                    {
                        a++;
                        b = 0;

                        if (a == m_NumberLevel)
                        {
                            a = 0;
                            b = 0;

                            m_SlantSymmetry = true;

                            break;
                        }
                    }

                    for (int n = 0; n < c_PartMap; n++)
                    {
                        if (n != 1 && n != 2)
                        {
                            m_listLevel[i, n] = a;
                        }
                        else
                        {
                            m_listLevel[i, n] = b;
                        }
                    }

                    i++;
                    b++;
                }

                while (m_AsymmetryUp == false)
                {
                    bool notrepeat = false;

                    while (notrepeat == false)
                    {
                        if (c == b)
                        {
                            c++;
                        }

                        if (c == m_NumberLevel)
                        {
                            b++;
                            c = 0;
                        }

                        if (b == m_NumberLevel)
                        {
                            a++;
                            b = 0;
                        }

                        if (c != b || a == m_NumberLevel) notrepeat = true;
                    }

                    if (a == m_NumberLevel)
                    {
                        a = 0;
                        b = 0;
                        c = 0;

                        m_AsymmetryUp = true;

                        break;
                    }

                    for (int n = 0; n < c_PartMap; n++)
                    {
                        if (n == 0)
                        {
                            m_listLevel[i, n] = c;
                        }

                        if (n == 1)
                        {
                            m_listLevel[i, n] = b;
                        }

                        if (n > 1)
                        {
                            m_listLevel[i, n] = a;
                        }
                    }

                    i++;
                    c++;
                }

                while (m_AssymetryDown == false)
                {
                    bool notrepeat = false;

                    while (notrepeat == false)
                    {
                        if (c == b)
                        {
                            c++;
                        }

                        if (c == m_NumberLevel)
                        {
                            b++;
                            c = 0;
                        }

                        if (b == m_NumberLevel)
                        {
                            a++;
                            b = 0;
                        }

                        if (c != b || a == m_NumberLevel) notrepeat = true;
                    }

                    if (a == m_NumberLevel)
                    {
                        a = 0;
                        b = 0;
                        c = 0;

                        m_AssymetryDown = true;

                        break;
                    }

                    for (int n = 0; n < c_PartMap; n++)
                    {
                        if (n < 2)
                        {
                            m_listLevel[i, n] = a;
                        }

                        if (n == 2)
                        {
                            m_listLevel[i, n] = c;
                        }

                        if (n == 3)
                        {
                            m_listLevel[i, n] = b;
                        }
                    }

                    i++;
                    c++;
                }

                while (m_AssymetryNext == false)
                {
                    bool notrepeat = false;

                    while (notrepeat == false)
                    {
                        if (d == c || (d == a && c == b) || (d == b && c == a))
                        {
                            d++;
                        }

                        if (d == m_NumberLevel)
                        {
                            c++;
                            d = 0;
                        }

                        if (c == m_NumberLevel)
                        {
                            b++;
                            c = 0;
                        }

                        if (b == a)
                        {
                            b++;
                        }

                        if (b == m_NumberLevel)
                        {
                            a++;
                            b = 0;
                        }

                        if (d != c && (d == a && c == b) == false && (d == b && c == a) == false && b != a)
                            notrepeat = true;
                    }

                    if (a == m_NumberLevel)
                    {
                        a = 0;
                        b = 0;
                        c = 0;
                        d = 0;
                        m_AssymetryNext = true;

                        break;
                    }

                    if (i < m_MaxLevel)
                    {
                        for (int n = 0; n < c_PartMap; n++)
                        {
                            if (n == 0)
                            {
                                m_listLevel[i, n] = d;
                            }

                            if (n == 1)
                            {
                                m_listLevel[i, n] = c;
                            }

                            if (n == 2)
                            {
                                m_listLevel[i, n] = b;
                            }

                            if (n == 3)
                            {
                                m_listLevel[i, n] = a;
                            }
                        }
                    }

                    i++;
                    d++;
                }
            }
        }

        private void PrintListLevel()
        {
            for (int i = 0; i < 256; i++)
            {
                Debug.Log(m_listLevel[i, 0] + ", " + m_listLevel[i, 1] + ", " + m_listLevel[i, 2] + ", " + m_listLevel[i, 3]);
            }
        }
    }
}