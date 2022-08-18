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
        public System.Action OnClickCommon = () => { };
        public System.Action OnClickRare = () => { };
        public System.Action OnClickUnique = () => { };
        public System.Action OnClickLegendary = () => { };

        public System.Action OnClickMove = () => { };
        public System.Action OnClickHold = () => { };
        public System.Action OnClickAttack = () => { };
        public System.Action OnClickStop = () => { };

        public void onClickMenu()
        {
            OnClickMenu();
        }
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
