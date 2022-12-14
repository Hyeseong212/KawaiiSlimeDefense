using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T m_Instance = null;
    //instance
    public static T i
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
                    //Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    //m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    Debug.LogWarning(typeof(T).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                m_Instance.Init();
            }
            return m_Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        OnAwake();
        if (m_Instance == null)
        {
            m_Instance = this as T;
            m_Instance.Init();
        }
    }
    protected virtual void OnAwake() { }
    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init() { }

    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        if (m_Instance != this) return;
        m_Instance = null;
    }

    public virtual void OnPause()
    {

    }

    public static bool IsInstance()
    {
        if (m_Instance == null)
            return false;

        return true;
    }
}