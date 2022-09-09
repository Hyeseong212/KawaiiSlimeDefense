using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup.MapSelect
{
    using Slash.Unity.DataBind.Core.Data;
    public class PopupCreateRoomContext : Context
    {
        public PopupCreateRoomContext()
        {

        }

        public System.Action onClickCreateRoom = () => { };
        public System.Action onClickExit = () => { };
        public void OnClickCreateRoom()
        {
            onClickCreateRoom();
        }
        public void OnClickExit()
        {
            onClickExit();
        }
   
    }
}
