using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;
using Photon.Pun;
using Photon.Realtime;
using UI.Popup.MapSelect;

public class PopupCreateRoom : MonoSingleton<PopupCreateRoom>
{
    [SerializeField] private ContextHolder _contextHolder;
    [SerializeField] GameObject enablePassword;
    [SerializeField] InputField RoomInput;
    bool isChecked =true;
    private PopupCreateRoomContext _context;
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new PopupCreateRoomContext();
        _contextHolder.Context = _context;
    }

    void Start()
    {
        _context.onClickCreateRoom = () =>
        {
            CreateRoom();
        };
    }

    public void SetPasswordEnable()
    {
        isChecked = !isChecked;
        enablePassword.SetActive(isChecked);
    }
    private void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text, new RoomOptions { MaxPlayers = 4 });

}
