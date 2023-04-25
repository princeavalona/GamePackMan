using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            {
                Debug.LogWarning("SingletonBase: object of type exists, instance will be destroyed = " + typeof(T).Name);

                Destroy(this);
                return;
            }
        }

        Instance = this as T;

        if (m_DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}