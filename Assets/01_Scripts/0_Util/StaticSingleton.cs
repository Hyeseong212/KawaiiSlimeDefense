using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSingleton<T> where T : StaticSingleton<T>, new()
{

    private static T m_Instance = null;
    public static T i
    {
        get
        {
            // Instance requiered for the first time, we look for it
            SingetonInit();
            return m_Instance;
        }
    }
    public static void SingetonInit()
    {
        if (m_Instance == null)
        {
            m_Instance = new T();
            m_Instance.Init();
        }
    }
    public virtual void Init() { }
    static public bool IsInstance() { return m_Instance != null; }
    static public void DeleteInstance() { m_Instance = null; }
}
