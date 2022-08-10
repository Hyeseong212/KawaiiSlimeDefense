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
        _context.OnClickMenu = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_MENU);

        }; 
        _context.OnClickTestCommon = () =>
        {
            UpgradeManager.i.CommonLevelUp();
        }; 
        _context.OnClickTestRare = () =>
        {
            UpgradeManager.i.RareLevelUp();
        };
        _context.OnClickTestUnique = () =>
        {
            UpgradeManager.i.UniqueLevelUp();

        }; 
        _context.OnClickTestLegendary = () =>
        {
            UpgradeManager.i.LegendaryLevelUp();
        };
    }
}
