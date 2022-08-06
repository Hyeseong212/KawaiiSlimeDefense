using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using Slash.Unity.DataBind.Core.Presentation;
using UI.Popup.Message;
public class PopupMessageOkCancel : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    private PopupMessageOkCancelConText _context;
    private Action<ButtonType> _action = (type) => { };
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new PopupMessageOkCancelConText();
        _contextHolder.Context = _context;
    }
    void Start()
    {

        _context.actionButton = (type) =>
        {
            _action(type);
        };
    }

    public void SetMessage(string msg, Action<ButtonType> action)
    {
        _context.SetValue("Text", msg);
        if (action != null) _action += action;
    }

    static public PopupMessageOkCancel ShowButtonOKCancel_ID(int ID, Action<ButtonType> action = null)
    {
        //return ShowButtonOKCancel(StrMgr.GetStr(ID), action);
        return ShowButtonOKCancel(null, action);
    }

    static public PopupMessageOkCancel ShowButtonOKCancel(string msg, Action<ButtonType> action)
    {
        PopupMessageOkCancel button = PopupManager.i.ShowPopup<PopupMessageOkCancel>(_type.E_POPUP.POPUP_BUTTON_OKCANCEL);
        button.SetMessage(msg, (type) => {
            PopupManager.i.BackPopup();
            if (action != null) action(type);
        });
        return button;
    }
}
