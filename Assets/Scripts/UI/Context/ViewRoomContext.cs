using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.Lobby
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewRoomContext : Context
    {
        public ViewRoomContext()
        {

        }
        public System.Action onClickBackToLobby = () => { };
        public System.Action onClickSendTxt = () => { };
        public System.Action onClickGameStart = () => { };
        public void OnClickSendTxt()
        {
            onClickSendTxt();
        }
        public void OnClickBackToLobby()
        {
            onClickBackToLobby();
        }
        public void OnClickGameStart()
        {
            onClickGameStart();
        }
    }
}
