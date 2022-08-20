using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerPanel : MonoSingleton<UnitControllerPanel>
{
    [Header("Building")]
    [SerializeField] GameObject[] upgradeBuildingObject;   
    [SerializeField] GameObject[] gambleBuildingObject;
    [Header("Unit")]
    [SerializeField] GameObject[] unitSelectedDeactivate;
    [SerializeField] GameObject[] unitSelectedActivate;
    [SerializeField] GameObject[] unitSkillImg;
    public void UnitSelected()
    {
        if (RTSUnitController.i.selectedUnitList.Count > 0)
        {
            for (int i = 0; i < unitSelectedDeactivate.Length; i++)
            {
                unitSelectedDeactivate[i].SetActive(true);
            }
            UnitStatusInPanel();
        }
    }
    public void UnitStatusInPanel()
    {
        if (RTSUnitController.i.selectedUnitList.Count > 0)
        {
            if (RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.ForcedAttack ||
                RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.Attack)
            {
                unitSelectedActivate[0].SetActive(false);
                unitSelectedActivate[1].SetActive(false);
                unitSelectedActivate[2].SetActive(true);
                unitSelectedActivate[3].SetActive(false);
            }
            else if (RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.Stop)
            {
                unitSelectedActivate[0].SetActive(false);
                unitSelectedActivate[1].SetActive(false);
                unitSelectedActivate[2].SetActive(false);
                unitSelectedActivate[3].SetActive(true);

            }
            else if (RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.Hold)
            {
                unitSelectedActivate[0].SetActive(false);
                unitSelectedActivate[1].SetActive(true);
                unitSelectedActivate[2].SetActive(false);
                unitSelectedActivate[3].SetActive(false);
            }
            else if (RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.Move ||
                RTSUnitController.i.selectedUnitList[0].shooter.status == SlimeStatus.ForcedMove)
            {
                unitSelectedActivate[0].SetActive(true);
                unitSelectedActivate[1].SetActive(false);
                unitSelectedActivate[2].SetActive(false);
                unitSelectedActivate[3].SetActive(false);
            }
        }
    }
    public void UpgradeBuildingClicked()
    {
        for(int i = 0; i < upgradeBuildingObject.Length; i++)
        {
            upgradeBuildingObject[i].SetActive(true);
        }
    }
    public void TokenBuildingClicked()
    {
        for (int i = 0; i < gambleBuildingObject.Length; i++)
        {
            gambleBuildingObject[i].SetActive(true);
        }
    }
    public void BuildingDeselected()
    {
        for (int i = 0; i < upgradeBuildingObject.Length; i++)
        {
            upgradeBuildingObject[i].SetActive(false);
        }
        for (int i = 0; i < gambleBuildingObject.Length; i++)
        {
            gambleBuildingObject[i].SetActive(false);
        }

    }
    public void UnitDeselected()
    {
        for (int i = 0; i < unitSelectedActivate.Length; i++)
        {
            unitSelectedActivate[i].SetActive(false);
        }
        for (int i = 0; i < unitSelectedDeactivate.Length; i++)
        {
            unitSelectedDeactivate[i].SetActive(false);
        }
    }

    public void ToolTipCommonUpgrade()
    {
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1002));
    }
    public void ToolTipRareUpgrade()
    {
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1003));
    }
    public void ToolTipUniqueUpgrade()
    {
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1004));
    }
    public void ToolTipLegendaryUpgrade()
    {
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1005));
    }
    public void ToolTipGoldGamble()
    {
        int i = 0;
        if (EnemySpawner.i.currentWave ==0)
        {
            i = 1;
        }
        else
        {
            i = EnemySpawner.i.currentWave;
        }
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1006)+"\n"+ 
            "(금화 "+ (i * 10).ToString()+"를(을) 소모하여 " + (-(i*15)-10).ToString()+ "~+"+ ((i * 35) + 10).ToString() + "의 금화를 얻습니다)");
    }
    public void ToolTipTokenGambleUpgrade()
    {
        UIViewGame.i.ToolTipTextSetter(AddresablesDataManager.i.GetString(1007));
    }

    public void PointerExit()
    {
        UIViewGame.i.ToolTipTextOff();
    }
}
