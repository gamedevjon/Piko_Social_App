using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    /// <summary>
    /// the instance field
    /// </summary>
    private static T _instance;

    /// <summary>
    /// The Instance property
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning("Creating a temp. instance of " + typeof(T).ToString());
                GameObject go = new GameObject("Temp Instance of " + typeof(T).ToString());
                go.AddComponent<T>();
                _instance = go.GetComponent<T>();

                if (_isInitialized == false)
                {
                    _isInitialized = true;
                    _instance.Init();
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// Is the manager initialized
    /// </summary>
    private static bool _isInitialized = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            DestroyImmediate(this);
        }

        if (_isInitialized == false)
        {
            _isInitialized = true;
            _instance.Init();
        }
    }

    /// <summary>
    /// This function is called when Init is used the first time. Use this for initialization.
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// Assign the instance to null so that it's not references when the user quits. 
    /// </summary>
    private void OnApplicationQuit()
    {
        _instance = null;
    }
}
