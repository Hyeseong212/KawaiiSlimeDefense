using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    protected static NetWorkManager m_Instance = null;
    //instance
    public static NetWorkManager i
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(NetWorkManager)) as NetWorkManager;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
                    //Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    //m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    Debug.LogWarning(typeof(NetWorkManager).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject(typeof(NetWorkManager).ToString(), typeof(NetWorkManager)).GetComponent<NetWorkManager>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(NetWorkManager).ToString());
                    }
                }
                m_Instance.Init();
            }
            return m_Instance;
        }
    }
    private void Awake()
    {
        OnAwake();
        if (m_Instance == null)
        {
            m_Instance = this as NetWorkManager;
            m_Instance.Init();
        }
    }
    protected virtual void OnAwake() { }
    public virtual void Init() { }
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
    /// <summary>
    /// 네트워크매니저 싱글톤화
    /// </summary>
    public Text StatusTxt;
    private void Start()
    {
        Connect();
    }
    private void Update()
    {
        if (StatusTxt != null)
        {
            StatusTxt.text = PhotonNetwork.NetworkClientState.ToString();
        }
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
}
