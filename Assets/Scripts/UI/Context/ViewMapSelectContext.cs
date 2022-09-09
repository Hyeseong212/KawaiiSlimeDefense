using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.MapSelect
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewMapSelectContext : Context
    {
        public ViewMapSelectContext()
        {

        }

        public System.Action onClickRight = () => { };
        public System.Action onClickLeft = () => { };
        public System.Action onClickCreateRoom = () => { };
        public System.Action onClickJoinRoom = () => { };

        public void OnClickRight()
        {
            onClickRight();
        }
        public void OnClickLeft()
        {
            onClickLeft();
        }
        public void OnClickCreateRoom()
        {
            onClickCreateRoom();
        }
        public void OnClickJoinRoom()
        {
            onClickJoinRoom();
        }
    }
}
