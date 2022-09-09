using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.MainMenu
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewMainMenuContext : Context
    {
        public ViewMainMenuContext()
        {

        }

        public System.Action onClickTouch = () => { };
 
        public void OnClickTouch()
        {
            onClickTouch();
        }
    }
}
