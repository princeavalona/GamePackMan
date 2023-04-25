using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacManGame
{
    public enum TypeSymmetry
    {
        NoneSymmetry,
        SymmetryType,
        AsymmetryType
    }

    public class PrefabMap : MonoBehaviour
    {        
        [SerializeField] private TypeSymmetry m_Symmetry;
        public TypeSymmetry Symmetry => m_Symmetry; 
    }
}