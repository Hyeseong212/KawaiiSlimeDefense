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

    [SerializeField] private Text levelTxt;
    [SerializeField] private Text goldTxt;
    [SerializeField] private Slider Exp;
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewMainMenuContext();
        _contextHolder.Context = _context;
    }
    void Start()
    {
        levelTxt.text = "Lv." + UserDataController.i.userData1.Level;
        goldTxt.text = UserDataController.i.userData1.Money;
        for(int i = 0; i< UserDataController.i.expTotalDataList.Count; i++)
        {
            if(UserDataController.i.expTotalDataList[i].Level == UserDataController.i.userData1.Level)
            {
                Exp.value = float.Parse(UserDataController.i.userData1.EXP) / float.Parse(UserDataController.i.expTotalDataList[i].Exp);
            }
        }
        NetWorkManager.i.StatusTxt = statusTxt;
        _context.onClickTouch = () =>
        {

        };
    }
}
