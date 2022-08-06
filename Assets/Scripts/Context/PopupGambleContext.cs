using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup.InGame
{
    using Slash.Unity.DataBind.Core.Data;
    public class PopupGambleContext : Context
    {
        public PopupGambleContext()
        {

        }
        public System.Action onClickTestDelegate = () => { };
        public void onClickTest()
        {
            Debug.Log("--------- onClickTouch");
            onClickTestDelegate();
        }
    }
}
