using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerPanel : MonoSingleton<UnitControllerPanel>
{
    [Header("Building")]
    [SerializeField] GameObject[] upgradeBuildingObject;   
    [SerializeField] GameObject[] gambleBuildingObject;
    [Header("Unit")]
    [SerializeField] GameObject[] unitSelected;
    [SerializeField] GameObject[] unitSkillImg;
    public void UnitSelected()
    {

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
            upgradeBuildingObject[i].SetActive(true);
        }
    }
    public void Deselected()
    {
        for (int i = 0; i < upgradeBuildingObject.Length; i++)
        {
            upgradeBuildingObject[i].SetActive(false);
        }
        for (int i = 0; i < gambleBuildingObject.Length; i++)
        {
            upgradeBuildingObject[i].SetActive(false);
        }
    }
}
