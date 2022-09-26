using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Slash.Unity.DataBind.Core.Presentation;
using UI.View.Lobby;
public class UIViewRoom : MonoSingleton<UIViewRoom>
{
    [SerializeField] private ContextHolder _contextHolder;
    private ViewRoomContext _context;
    [SerializeField] Text statusTxt;
    [SerializeField] Dropdown InGameDifficulty;

    private void Awake()
    {
        if (_contextHolder == null) _contextHolder = GetComponent<ContextHolder>();

        _context = new ViewRoomContext();
        _contextHolder.Context = _context;
    }
    private void Start()
    {
        NetWorkManager.i.StatusTxt = statusTxt;
    }
    public void OnDifficultyChange()
    {
        GlobalOptions.i.options.Difficulty = InGameDifficulty.value;
    }
}
