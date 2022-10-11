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

    [Header("테스트용")]
    [SerializeField] GameObject RoomButton;
    [SerializeField] GameObject RoomListPanel;
    #region 방리스트 갱신
    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        //MyListRenewal();
    }
    //void MyListRenewal()
    //{
    //    // 최대페이지
    //    maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

    //    // 이전, 다음버튼
    //    PreviousBtn.interactable = (currentPage <= 1) ? false : true;
    //    NextBtn.interactable = (currentPage >= maxPage) ? false : true;

    //    // 페이지에 맞는 리스트 대입
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
        //-로비에 접속 시
        //-새로운 룸이 만들어질 경우
        //- 룸이 삭제되는 경우
        //-룸의 IsOpen 값이 변화할 경우(아예 RoomInfo 내 데이터가 바뀌는 경우 전체일 수도 있습니다)
        int roomCount = roomList.Count;//룸전체를 파괴하고 룸갯수만큼 생성
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

    #region 리스트 갱신

    #endregion
    #region 서버연결

    void Update()
    {
        if (StatusText != null && LobbyInfoText != null)
        {
            StatusText.text = PhotonNetwork.NetworkClientState.ToString();
            LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
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


    #region 방
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
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    void RoomRenewal()
    {
        //ListText.text = "";
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //    ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        //RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }
    #endregion


    #region 채팅
    public void Send(string _chattxt)
    {
        PV.RPC("ChatRPC", RpcTarget.All, UserDataController.i.userData1.Name + " : " + ChatInput.text);
        ChatInput.text = "";
    }
    [SerializeField] GameObject Chat;
    [SerializeField] GameObject ChatParent;
    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
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
        //if (!isInput) // 꽉차면 한칸씩 위로 올림
        //{
        //    for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
        //    ChatText[ChatText.Length - 1].text = msg;
        //}
    }
    #endregion

    #region (테스트)
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
    #region 인스터스화(테스트용)
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
