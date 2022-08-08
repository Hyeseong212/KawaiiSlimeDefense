using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup.InGame
{
    using Slash.Unity.DataBind.Core.Data;
    public class PopupMenuContext : Context
    {
        public PopupMenuContext()
        {

        }
        public System.Action onClickOptions = () => { };
        public System.Action onClickQuit = () => { };
        public System.Action onClickExit = () => { };
        public void OnClickOptions()
        {
            onClickOptions();
        }
        public void OnClickQuit()
        {
            onClickQuit();
        }
        public void OnClickExit()
        {
            onClickExit();
        }
   
    }
}
