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

        public System.Action OnClickLevelUp = () => { };
        public System.Action OnClickGamble = () => { };
        public void onClickLevelUp()
        {
            Debug.Log("--------- OnClickLevelUp");
            OnClickLevelUp();
        }
        public void onClickGamble()
        {
            Debug.Log("--------- OnClickGamble");
            OnClickGamble();
        }
    }
}
