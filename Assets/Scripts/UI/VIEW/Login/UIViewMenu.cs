using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;

using UI.View.Menu;
public class UIViewMenu : MonoSingleton<UIViewMenu>
{
    [SerializeField] private ContextHolder _contextHolder;
    [SerializeField] private Text statusTxt;
    private ViewMenuContext _context;
    WaitForSeconds fakeLoadingTime = new WaitForSeconds(3f);
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewMenuContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
        StartCoroutine("LoadingPopup");
        _context.onClickTouch = () =>
        {
            NetWorkManager.i.Connect();
            GSceneManager.i.MoveSceneAsync(GSceneManager.SCENE_TYPE.MainMenu);
        };
    }
    IEnumerator LoadingPopup()
    {
        PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_LOADING);
        yield return fakeLoadingTime;
        PopupManager.i.BackPopup(_type.E_POPUP.POPUP_LOADING);
    }
}
