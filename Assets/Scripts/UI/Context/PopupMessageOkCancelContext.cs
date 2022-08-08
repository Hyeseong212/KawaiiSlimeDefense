using System.Collections;
using UnityEngine;



namespace UI.Popup.Message
{
    using Slash.Unity.DataBind.Core.Data;
    using System;

    public enum ButtonType
    {
        OK = 0,
        Cancel = 1,
        Close,
    }
    public class PopupMessageOkCancelConText : Context
    {
        private readonly Property<string> TextProperty = new Property<string>();

        // Start is called before the first frame update

        public PopupMessageOkCancelConText()
        {

        }
        public string Text { get { return this.TextProperty.Value; } set { this.TextProperty.Value = value; } }
        public Action<ButtonType> actionButton = (type) => { };

        public void onClickOK()
        {
            Debug.Log("--------- onClickOK");

            actionButton(ButtonType.OK);
        }
        public void onClickCancel()
        {
            Debug.Log("--------- onClickCancel");

            actionButton(ButtonType.Cancel);
        }
    }
}