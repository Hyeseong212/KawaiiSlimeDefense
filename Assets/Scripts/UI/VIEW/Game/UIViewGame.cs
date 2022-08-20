using UnityEngine;
using System;


using Slash.Unity.DataBind.Core.Presentation;

using UI.View.InGame;
public class UIViewGame : MonoSingleton<UIViewGame>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewGameContext _context;
    [SerializeField] private GameObject warningToolbar;
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewGameContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {
        //메뉴
        _context.OnClickMenu = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_MENU);

        }; 
        //업그레이드건물 버튼
        _context.OnClickCommon = () =>
        {
            UpgradeManager.i.CommonLevelUp();
        }; 
        _context.OnClickRare = () =>
        {
            UpgradeManager.i.RareLevelUp();
        };
        _context.OnClickUnique = () =>
        {
            UpgradeManager.i.UniqueLevelUp();

        }; 
        _context.OnClickLegendary = () =>
        {
            UpgradeManager.i.LegendaryLevelUp();
        };
        //도박소 건물 버튼
        _context.OnClickGambleGold = () =>
        {
            GambleManager.i.GoldGamble();
        };
        _context.OnClickGambleToken = () =>
        {
            GambleManager.i.TokenGamble();
        };
        //유닛 컨트롤
        _context.OnClickMove = () =>
        {
            MoveBtn();
        };
        _context.OnClickHold = () =>
        {
            HoldBtn();
        };
        _context.OnClickAttack = () =>
        {
            AttackBtn();
        };
        _context.OnClickStop = () =>
        {
            StopBtn();
        };

    }
    void MoveBtn()
    {
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++) 
        {
            UnitControllerPanel.i.UnitDeselected();
            RTSUnitController.i.selectedUnitList[i].isWaitForCommand = true;
            RTSUnitController.i.selectedUnitList[i].shooter.status = SlimeStatus.ForcedMove;
        }
    }
    void HoldBtn()
    {
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++)
        {
            RTSUnitController.i.selectedUnitList[i].shooter.status = SlimeStatus.Hold;
            RTSUnitController.i.selectedUnitList[i].Stop();
        }
    }
    void AttackBtn()
    {
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++)
        {
            UnitControllerPanel.i.UnitDeselected();
            RTSUnitController.i.selectedUnitList[i].isWaitForCommand = true;
            RTSUnitController.i.selectedUnitList[i].shooter.status = SlimeStatus.ForcedAttack;
        }
    }
    void StopBtn()
    {
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++)
        {
            RTSUnitController.i.selectedUnitList[i].shooter.status = SlimeStatus.Stop;
            RTSUnitController.i.selectedUnitList[i].Stop();
        }
    }
    public void WarningTextSetter(string msg)
    {
        CancelInvoke("SetOff");
        warningToolbar.SetActive(true);
        _context.SetValue("Text", msg);
        Invoke("SetOff", 5f);
    }
    void SetOff()
    {
        warningToolbar.SetActive(false);
    }
}
