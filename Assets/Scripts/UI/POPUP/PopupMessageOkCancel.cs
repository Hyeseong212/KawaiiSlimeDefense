using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using Slash.Unity.DataBind.Core.Presentation;
using UI.Popup.Message;
public class UIPopupMessageButton : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    private PopupMessageOkCancel _context;
    private Action<ButtonType> _action = (type) => { };

    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new PopupMessageOkCancel();
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

    static public UIPopupMessageButton ShowButtonOKCancel_ID(int titleID, int ID, Action<ButtonType> action = null)
    {
        return ShowButtonOKCancel(StrMgr.GetStr(ID), action);
    }

    static public UIPopupMessageButton ShowButtonOKCancel(string msg, Action<ButtonType> action)
    {
        UIPopupMessageButton button = PopupManager.i.ShowPopup<UIPopupMessageButton>(_type.E_POPUP.POPUP_BUTTON_OKCANCEL);
        button.SetMessage(msg, (type) => {
            PopupManager.i.BackPopup();
            if (action != null) action(type);
        });
        return button;
    }
}
