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
        public System.Action OnClickTestCommon = () => { };
        public System.Action OnClickTestRare = () => { };
        public System.Action OnClickTestUnique = () => { };
        public System.Action OnClickTestLegendary = () => { };

        public void onClickMenu()
        {
            OnClickMenu();
        }
        public void onClickTestCommon()
        {
            OnClickTestCommon();
        }
        public void onClickTestRare()
        {
            OnClickTestRare();
        }
        public void onClickTestUnique()
        {
            OnClickTestUnique();
        }
        public void onClickTestLegendary()
        {
            OnClickTestLegendary();
        }
    }
}
