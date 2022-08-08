using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup.InGame
{
    using Slash.Unity.DataBind.Core.Data;
    public class PopupLevelUpContext : Context
    {
        public PopupLevelUpContext()
        {

        }
        public System.Action onClickCommonLevelUp = () => { };
        public System.Action onClickRareLevelUp = () => { };
        public System.Action onClickUniqueLevelUp = () => { };
        public System.Action onClickLegendaryLevelUp = () => { };
        public void OnClickCommonLevelUp()
        {
            Debug.Log("--------- onClickCommonLevelUp");
            onClickCommonLevelUp();
        }
        public void OnClickRareLevelUp()
        {
            Debug.Log("--------- onClickRareLevelUp");
            onClickRareLevelUp();
        }
        public void OnClickUniqueLevelUp()
        {
            Debug.Log("--------- onClickUniqueLevelUp");
            onClickUniqueLevelUp();
        }
        public void OnClickLegendaryLevelUp()
        {
            Debug.Log("--------- onClickLegendaryLevelUp");
            onClickLegendaryLevelUp();
        }
    }
}
