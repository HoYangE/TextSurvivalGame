using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<T>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject(typeof(T).ToString());
                    _instance = newSingleton.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            // if(transform.parent != null && transform.parent.name != null)
            //     DontDestroyOnLoad(transform.root.gameObject);
            // else
            //     DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
