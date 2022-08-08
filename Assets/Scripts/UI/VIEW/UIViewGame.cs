using UnityEngine;
using System;


using Slash.Unity.DataBind.Core.Presentation;

using UI.View.InGame;
public class UIViewGame : MonoSingleton<UIViewGame>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewGameContext _context;

    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewGameContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {

        _context.OnClickLevelUp = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_LEVELUP);
        };
        _context.OnClickGamble = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_GAMBLE);
        };
    }
}
