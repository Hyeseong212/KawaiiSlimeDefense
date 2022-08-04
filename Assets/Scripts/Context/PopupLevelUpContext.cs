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
        public System.Action onClickCommonLevelUp = () => { };
        public System.Action onClickRareLevelUp = () => { };
        public System.Action onClickUniqueLevelUp = () => { };
        public System.Action onClickLegendaryLevelUp = () => { };
        public void OnClickCommonLevelUp()
        {
            Debug.Log("--------- onClickOption");
            onClickCommonLevelUp();
        }
        public void OnClickRareLevelUp()
        {
            Debug.Log("--------- onClickQuit");
            onClickRareLevelUp();
        }
        public void OnClickUniqueLevelUp()
        {
            Debug.Log("--------- onClickExit");
            onClickUniqueLevelUp();
        }
   
    }
}
