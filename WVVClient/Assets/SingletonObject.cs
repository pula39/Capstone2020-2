using UnityEngine;
using System.Collections;

public abstract class SingletonObject<T> : MonoBehaviour where T : Component 
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            CreateEagerly();

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

	// Use this for initialization
	protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
        DontDestroyOnLoad(gameObject);

        PostAwake();
	}

    protected virtual void PostAwake()
    {
        /* Nothing to do */
    }

    public static void CreateEagerly()
    {
        if (_instance == null)
        {
            GameObject gameobj = new GameObject();
            gameobj.name = typeof(T).Name;
            gameobj.AddComponent<T>();
            DontDestroyOnLoad(gameobj);

            _instance = gameobj.GetComponent<T>();
        }
    }

    public static bool InstanceAlive()
    {
        return _instance == false;
    }

    public static void Destroy()
    {
        if (_instance == false)
        {
            Destroy(_instance.gameObject);
        }

        _instance = null;
    }
}
