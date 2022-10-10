using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Slash.Unity.DataBind.Core.Presentation;
using UI.View.Lobby;
public class UIViewLobby : MonoSingleton<UIViewLobby>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewLobbyContext _context;
    [SerializeField] Text statusTxt;
    [SerializeField] List<GameObject> NickNamePanelList;
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewLobbyContext();
        _contextHolder.Context = _context;
    }
    private void Start()
    {
        StartCoroutine("CheckPlayerCount");
        NetWorkManager.i.StatusTxt = statusTxt;
        _context.onClickRoomCreate = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_CREATE_ROOM);
        };
        _context.onClickTestBtn = () =>
        {
        };
    }
    private void Update()
    {
        //새로 한명이 들어올때 오브젝트 추가
    }
    #region 플레이어 체크
    WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
    IEnumerator CheckPlayerCount()
    {
        for(int i = 0; i< NickNamePanelList.Count; i++)
        {
            NickNamePanelList[i].SetActive(false);
        }
        for(int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            NickNamePanelList[i].SetActive(true);
            UserData userData = JsonUtility.FromJson<UserData>(LobbyNetwork.i.userDatas[i]);
            NickNamePanelList[i].GetComponentInChildren<Text>().text = userData.Name;
        }
        yield return delay;
        StartCoroutine("CheckPlayerCount");
    }
    #endregion
}
