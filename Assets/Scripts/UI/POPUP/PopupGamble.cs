using System;
using UnityEngine;

using Slash.Unity.DataBind.Core.Presentation;
using UI.Popup.InGame;
public class PopupGamble : MonoBehaviour
{
    [SerializeField] private ContextHolder _contextHolder;
    private PopupGambleContext _gambleContext;

    public Action<bool> actionResult = delegate { };
    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _gambleContext = new PopupGambleContext();
        _contextHolder.Context = _gambleContext;

    }
    public void onClickLevelUp()
    {
        Debug.Log("-------ClickTestDelegate");
        _gambleContext.onClickTestDelegate();
    }
}   
