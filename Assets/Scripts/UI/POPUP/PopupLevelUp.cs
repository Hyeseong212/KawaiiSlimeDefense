using System;
using UnityEngine;

using Slash.Unity.DataBind.Core.Presentation;
using UI.Popup.InGame;
public class PopupLevelUp : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    private PopupLevelUpContext _levelUpContext;

    public Action<bool> actionResult = delegate { };
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _levelUpContext = new PopupLevelUpContext();
        _contextHolder.Context = _levelUpContext;

    }
    private void Start()
    {
         _levelUpContext.onClickCommonLevelUp = () =>
        {

        };

        _levelUpContext.onClickRareLevelUp = () =>
        {

        };

        _levelUpContext.onClickUniqueLevelUp = () =>
        {

        };

        _levelUpContext.onClickLegendaryLevelUp = () =>
        {

        };

    }
}   
