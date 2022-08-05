using System;
using UnityEngine;

using Slash.Unity.DataBind.Core.Presentation;
using UI.Popup.InGame;
public class PopupMenu : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    private PopupMenuContext _menuContext;

    public Action<bool> actionResult = delegate { };
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _menuContext = new PopupMenuContext();
        _contextHolder.Context = _menuContext;

    }
    private void Start()
    {
        _menuContext.onClickOptions = () =>
        {

        };

        _menuContext.onClickQuit = () =>
        {

        };

        _menuContext.onClickExit = () =>
        {
            PopupMessageOkCancel.ShowButtonOKCancel_ID(1000);
        };
    }
}   
