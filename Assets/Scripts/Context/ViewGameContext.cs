using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.InGame
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewGameContext : Context
    {
        public ViewGameContext()
        {

        }

        public System.Action OnClickMenu = () => { };
 
        public void onClickMenu()
        {
            Debug.Log("--------- onClickMenu");
            OnClickMenu();
        }
    }
}
