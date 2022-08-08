using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.Menu
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewMenuContext : Context
    {
        public ViewMenuContext()
        {

        }

        public System.Action onClickTouch = () => { };
 
        public void OnClickTouch()
        {
            onClickTouch();
        }
    }
}
