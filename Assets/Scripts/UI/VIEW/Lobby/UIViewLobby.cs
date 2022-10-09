using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;
using UI.View.Lobby;
public class UIViewLobby : MonoSingleton<UIViewLobby>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewLobbyContext _context;
    [SerializeField] Text statusTxt;

    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewLobbyContext();
        _contextHolder.Context = _context;
    }
    private void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
        _context.onClickRoomCreate = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_CREATE_ROOM);
        };
    }
}
