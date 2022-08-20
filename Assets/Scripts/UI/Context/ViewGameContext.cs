using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.View.InGame
{
    using Slash.Unity.DataBind.Core.Data;
    public class ViewGameContext : Context
    {
        private readonly Property<string> TextProperty = new Property<string>();
        private readonly Property<string> ToolTipTextProperty = new Property<string>();

        public ViewGameContext()
        {

        }
        public string Text { get { return this.TextProperty.Value; } set { this.TextProperty.Value = value; } }
        public string ToolTipText { get { return this.ToolTipTextProperty.Value; } set { this.ToolTipTextProperty.Value = value; } }
        //�޴�
        public System.Action OnClickMenu = () => { };
        //��Ʈ�ѷ� �г� ���׷��̵� �ǹ� ��ư
        public System.Action OnClickCommon = () => { };
        public System.Action OnClickRare = () => { };
        public System.Action OnClickUnique = () => { };
        public System.Action OnClickLegendary = () => { };
        //��Ʈ�ѷ� �г� ���ڼ� �ǹ� ��ư
        public System.Action OnClickGambleGold = () => { };
        public System.Action OnClickGambleToken = () => { };
        //��Ʈ�ѷ� �г� ���� ��ư
        public System.Action OnClickMove = () => { };
        public System.Action OnClickHold = () => { };
        public System.Action OnClickAttack = () => { };
        public System.Action OnClickStop = () => { };

        //�޴�
        public void onClickMenu()
        {
            OnClickMenu();
        }
        //��Ʈ�ѷ� �г� ���׷��̵� �ǹ� ��ư
        public void onClickCommon()
        {
            OnClickCommon();
        }
        public void onClickRare()
        {
            OnClickRare();
        }
        public void onClickUnique()
        {
            OnClickUnique();
        }
        public void onClickLegendary()
        {
            OnClickLegendary();
        }
        //��Ʈ�ѷ� �г� ���ڼ� �ǹ� ��ư
        public void onClickGambleGold()
        {
            OnClickGambleGold();
        }
        public void onClickGambleToken()
        {
            OnClickGambleToken();
        }

        //��Ʈ�ѷ� �г� ���� ��ư
        public void onClickMove()
        {
            OnClickMove();
        }
        public void onClickHold()
        {
            OnClickHold();
        }
        public void onClickAttack()
        {
            OnClickAttack();
        }

        public void onClickStop()
        {
            OnClickStop();
        }

    }
}
