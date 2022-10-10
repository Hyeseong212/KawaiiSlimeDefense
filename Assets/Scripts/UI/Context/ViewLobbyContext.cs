using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.Lobby
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewLobbyContext : Context
    {
        public ViewLobbyContext()
        {

        }

        public System.Action onClickRoomCreate = () => { };
        public System.Action onClickTestBtn = () => { };
 
        public void OnClickRoomCreate()
        {
            onClickRoomCreate();
        }
        public void OnClickTestBtn()
        {
            onClickTestBtn();
        }
    }
}
