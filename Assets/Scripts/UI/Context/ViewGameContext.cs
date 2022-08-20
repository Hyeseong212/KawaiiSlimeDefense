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
        //메뉴
        public System.Action OnClickMenu = () => { };
        //컨트롤러 패널 업그레이드 건물 버튼
        public System.Action OnClickCommon = () => { };
        public System.Action OnClickRare = () => { };
        public System.Action OnClickUnique = () => { };
        public System.Action OnClickLegendary = () => { };
        //컨트롤러 패널 도박소 건물 버튼
        public System.Action OnClickGambleGold = () => { };
        public System.Action OnClickGambleToken = () => { };
        //컨트롤러 패널 유닛 버튼
        public System.Action OnClickMove = () => { };
        public System.Action OnClickHold = () => { };
        public System.Action OnClickAttack = () => { };
        public System.Action OnClickStop = () => { };

        //메뉴
        public void onClickMenu()
        {
            OnClickMenu();
        }
        //컨트롤러 패널 업그레이드 건물 버튼
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
        //컨트롤러 패널 도박소 건물 버튼
        public void onClickGambleGold()
        {
            OnClickGambleGold();
        }
        public void onClickGambleToken()
        {
            OnClickGambleToken();
        }

        //컨트롤러 패널 유닛 버튼
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
