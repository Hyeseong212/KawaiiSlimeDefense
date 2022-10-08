using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Slash.Unity.DataBind.Core.Presentation;

using UI.Popup.CreateNewUser;
public class PopupCreateNewKey : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    [SerializeField] private InputField ID;
    [SerializeField] private InputField Password;
    private PopupCreateNewKeyContext _context;
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new PopupCreateNewKeyContext();
        _contextHolder.Context = _context;
    }

    void Start()
    {
        _context.onClickCreateNewKey = () =>
        {
            if (string.IsNullOrEmpty(ID.text))
            {
                Debug.Log("ID를 입력해주세요");
                return;
            }
            else if (string.IsNullOrWhiteSpace(ID.text))
            {
                Debug.Log("공백아이디는 사용 할 수 없습니다");
                return;
            }
            else if (string.IsNullOrEmpty(Password.text))
            {
                Debug.Log("Password를 입력해주세요");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Password.text))
            {
                Debug.Log("공백패스워드는 사용 할 수 없습니다");
                return;
            }
            else
            {
                UserDataController.i.CreateNewKey(ID.text, Password.text);
            }
        };
        _context.onClickBack = () =>
        {
            PopupManager.i.BackPopup(_type.E_POPUP.POPUP_CREATENEWKEY);
        };
    }
}
