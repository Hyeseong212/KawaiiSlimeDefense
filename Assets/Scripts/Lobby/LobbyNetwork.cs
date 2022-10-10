using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LobbyNetwork : MonoBehaviourPunCallbacks,IPunObservable
{

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public Text WelcomeText;
    public Text LobbyInfoText;
    //public Button[] CellBtn;
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

    //int currentPage = 1, maxPage, multiple;


    //#region 방리스트 갱신
    //// ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    //public void MyListClick(int num)
    //{
    //    if (num == -2) --currentPage;
    //    else if (num == -1) ++currentPage;
    //    else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
    //    //MyListRenewal();
    //}

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

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    int roomCount = roomList.Count;
    //    for (int i = 0; i < roomCount; i++)
    //    {
    //        if (!roomList[i].RemovedFromList)
    //        {
    //            if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
    //            else myList[myList.IndexOf(roomList[i])] = roomList[i];
    //        }
    //        else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
    //    }
    //    //MyListRenewal();
    //}
    //#endregion

    #region 리스트 갱신

    #endregion
    #region 서버연결

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
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
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text, new RoomOptions { MaxPlayers = 4 });

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
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }
    #endregion


    #region 채팅
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion

    #region (테스트)
    public string json;
    public  List<string> userDatas = new List<string>();
    public void PlayerListBinding(string _userData)
    {
        userDatas.Add(_userData);
        json = JsonUtility.ToJson(userDatas);
        Debug.Log(json);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(json);
        else json = (string)stream.ReceiveNext();
    }
    public void PlayerListJsonToList()
    {
        userDatas = JsonUtility.FromJson<List<string>>(json);
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
