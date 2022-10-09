using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DG.Tweening;


using Slash.Unity.DataBind.Core.Presentation;

using UI.View.MapSelect;
public class UIViewMapSelect : MonoSingleton<UIViewMapSelect>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewMapSelectContext _context;
    [SerializeField] private GameObject mapContents;
    [SerializeField] List<GameObject> Maps;

    [SerializeField] GameObject rightBtn;
    [SerializeField] GameObject leftBtn;
    [SerializeField] Text statusTxt;
    [Header("ÆÐ³Îµé")]
    [SerializeField] GameObject MapSelectPanel;
    [SerializeField] GameObject LobbyPanel;
    [SerializeField] GameObject RoomPanel;
    float vectorx = 0;
    int i = 0;
    public override void Init()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewMapSelectContext();
        _contextHolder.Context = _context;
    }

    void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
        if (i == 0)
        {
            leftBtn.SetActive(false);
        }
        _context.onClickRight = () =>
        {
            i++;
            mapContents.GetComponent<RectTransform>().DOAnchorPos(-Maps[i].GetComponent<RectTransform>().anchoredPosition, 1f);
            if (i == 2)
            {
                rightBtn.SetActive(false);
                leftBtn.SetActive(true);
            }
            else if(i == 1)
            {
                leftBtn.SetActive(true);
            }
        };
        _context.onClickLeft = () =>
        {
            i--;
            mapContents.GetComponent<RectTransform>().DOAnchorPos(-Maps[i].GetComponent<RectTransform>().anchoredPosition, 1f);
            if (i == 0)
            {
                rightBtn.SetActive(true);
                leftBtn.SetActive(false);
            }
            else if(i == 1)
            {
                rightBtn.SetActive(true);
            }
        };
        _context.onClickCreateRoom = () =>
        {
            PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_CREATE_ROOM);
        };
        _context.onClickJoinRoom = () =>
        {
            MapSelectPanel.SetActive(false);
            LobbyPanel.SetActive(true);
        };
    }

}
