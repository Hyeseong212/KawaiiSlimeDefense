using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;

using UI.View.MainMenu;
public class UIViewMainMenu : MonoSingleton<UIViewMainMenu>
{
    [SerializeField] private ContextHolder _contextHolder;
    [SerializeField] private Text statusTxt;
    private ViewMainMenuContext _context;
    WaitForSeconds fakeLoadingTime = new WaitForSeconds(3f);
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewMainMenuContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
        _context.onClickTouch = () =>
        {
        };
    }
}
