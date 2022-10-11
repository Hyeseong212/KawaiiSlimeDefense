using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


[Serializable]
public class PlayerListSerialzing<T>
{
    [SerializeField]
    List<T> playerList;
    public List<T> ToList() { return playerList; }
    public PlayerListSerialzing(List<T> _playerList)
    {
        this.playerList = _playerList;
    }
}
public class LobbyNetwork : MonoBehaviourPunCallbacks,IPunObservable
{

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public Text WelcomeText;
    public Text LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text ListText;
    public Text RoomInfoText;
    public Text[] ChatText;
    public InputField ChatInput;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;

    List<RoomInfo> myList = new List<RoomInfo>();

    int currentPage = 1, maxPage, multiple;

    [Header("�׽�Ʈ��")]
    [SerializeField] GameObject RoomButton;
    [SerializeField] GameObject RoomListPanel;
    #region �渮��Ʈ ����
    // ����ư -2 , ����ư -1 , �� ����
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        //MyListRenewal();
    }
    //void MyListRenewal()
    //{
    //    // �ִ�������
    //    maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

    //    // ����, ������ư
    //    PreviousBtn.interactable = (currentPage <= 1) ? false : true;
    //    NextBtn.interactable = (currentPage >= maxPage) ? false : true;

    //    // �������� �´� ����Ʈ ����
    //    multiple = (currentPage - 1) * CellBtn.Length;
    //    for (int i = 0; i < CellBtn.Length; i++)
    //    {
    //        CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
    //        CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
    //        CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
    //    }
    //}
    List<GameObject> curRoomList = new List<GameObject>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //-�κ� ���� ��
        //-���ο� ���� ������� ���
        //- ���� �����Ǵ� ���
        //-���� IsOpen ���� ��ȭ�� ���(�ƿ� RoomInfo �� �����Ͱ� �ٲ�� ��� ��ü�� ���� �ֽ��ϴ�)
        int roomCount = roomList.Count;//����ü�� �ı��ϰ� �밹����ŭ ����
        for(int i = 0; i< curRoomList.Count; i++)
        {
            Destroy(curRoomList[i].gameObject);
        }
        for(int i=0; i < roomCount; i++)
        {
            GameObject roomBtn = Instantiate(RoomButton, transform.position, Quaternion.identity);
            curRoomList.Add(roomBtn);
            roomBtn.transform.SetParent(RoomListPanel.transform);
            roomBtn.GetComponent<Button>().onClick.AddListener(() => MyListClick(0));
            if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
            else myList[myList.IndexOf(roomList[i])] = roomList[i];

        }
        //int roomCount = roomList.Count;
        //for (int i = 0; i < roomCount; i++)
        //{
        //    if (!roomList[i].RemovedFromList)
        //    {
        //        if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
        //        else myList[myList.IndexOf(roomList[i])] = roomList[i];
        //    }
        //    else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        //}
        //MyListRenewal();
    }
    #endregion

    #region ����Ʈ ����

    #endregion
    #region ��������

    void Update()
    {
        if (StatusText != null && LobbyInfoText != null)
        {
            StatusText.text = PhotonNetwork.NetworkClientState.ToString();
            LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "�κ� / " + PhotonNetwork.CountOfPlayers + "����";
        }
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = UserDataController.i.userData1.Name;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName ;
        myList.Clear();
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }
    #endregion


    #region ��
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + UnityEngine.Random.Range(0, 100) : RoomInput.text, new RoomOptions { MaxPlayers = 4 });

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedRoom()
    {
        RoomPanel.SetActive(true);
        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public override void OnJoinRandomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    void RoomRenewal()
    {
        //ListText.text = "";
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //    ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        //RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "�� / " + PhotonNetwork.CurrentRoom.MaxPlayers + "�ִ�";
    }
    #endregion


    #region ä��
    public void Send(string _chattxt)
    {
        PV.RPC("ChatRPC", RpcTarget.All, UserDataController.i.userData1.Name + " : " + ChatInput.text);
        ChatInput.text = "";
    }
    [SerializeField] GameObject Chat;
    [SerializeField] GameObject ChatParent;
    [PunRPC] // RPC�� �÷��̾ �����ִ� �� ��� �ο����� �����Ѵ�
    void ChatRPC(string msg)
    {
        //bool isInput = false;
        GameObject chat = Instantiate(Chat, transform.position, Quaternion.identity);
        chat.transform.SetParent(ChatParent.transform);
        chat.GetComponent<Text>().text = msg; 
        //for (int i = 0; i < ChatText.Length; i++)
        //    if (ChatText[i].text == "")
        //    {
        //        isInput = true;
        //        ChatText[i].text = msg;
        //        break;
        //    }
        //if (!isInput) // ������ ��ĭ�� ���� �ø�
        //{
        //    for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
        //    ChatText[ChatText.Length - 1].text = msg;
        //}
    }
    #endregion

    #region (�׽�Ʈ)
    public string json;
    [SerializeField] public List<UserData> userDatas = new List<UserData>();
    public void PlayerListBinding(UserData _userData)
    {
        userDatas.Add(_userData);
        if (!string.IsNullOrEmpty(json))
        {
            json = JsonUtility.ToJson(new PlayerListSerialzing<UserData>(userDatas)) + json;
        }
        else
        {
            json = JsonUtility.ToJson(new PlayerListSerialzing<UserData>(userDatas));
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(json);
        else json = (string)stream.ReceiveNext();
    }
    public void PlayerListJsonToList()
    {
        userDatas = JsonUtility.FromJson<List<UserData>>(json);
    }

    #endregion
    #region �ν��ͽ�ȭ(�׽�Ʈ��)
    protected static LobbyNetwork m_Instance = null;
    public static LobbyNetwork i
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(LobbyNetwork)) as LobbyNetwork;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
                    //Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    //m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    Debug.LogWarning(typeof(LobbyNetwork).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject(typeof(LobbyNetwork).ToString(), typeof(LobbyNetwork)).GetComponent<LobbyNetwork>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(LobbyNetwork).ToString());
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
            m_Instance = this as LobbyNetwork;
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
    #endregion
}
