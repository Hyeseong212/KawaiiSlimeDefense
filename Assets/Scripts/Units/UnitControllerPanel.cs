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
}
