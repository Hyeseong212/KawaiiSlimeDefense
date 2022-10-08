using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup.CreateNewUser
{
    using Slash.Unity.DataBind.Core.Data;
    public class PopupCreateNewKeyContext : Context
    {
        public PopupCreateNewKeyContext()
        {

        }

        public System.Action onClickCreateNewKey = () => { };
        public System.Action onClickBack = () => { };
        public void OnClickCreateNewKey()
        {
            onClickCreateNewKey();
        }
        public void OnClickBack()
        {
            onClickBack();
        }
    }
}
