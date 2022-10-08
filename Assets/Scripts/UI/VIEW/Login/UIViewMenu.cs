using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;

using UI.View.Menu;
public class UIViewMenu : MonoSingleton<UIViewMenu>
{
    [SerializeField] private ContextHolder _contextHolder;
    [SerializeField] private Text statusTxt;
    [SerializeField] private InputField UserID;
    [SerializeField] private InputField UserKey;
    private ViewMenuContext _context;
    WaitForSeconds fakeLoadingTime = new WaitForSeconds(1f);
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewMenuContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
        NetWorkManager.i.Connect();
        StartCoroutine("LoadingPopup");
        _context.onClickCreateNewKey = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_CREATENEWKEY);
        };
        _context.onClickLogin = () =>
        {
            UserDataController.i.UserDataInit(UserID.text ,UserKey.text);
            //GSceneManager.i.MoveSceneAsync(GSceneManager.SCENE_TYPE.MainMenu);
        };
    }
    IEnumerator LoadingPopup()
    {
        PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_LOADING);
        yield return fakeLoadingTime;
        PopupManager.i.BackPopup(_type.E_POPUP.POPUP_LOADING);
    }
}
