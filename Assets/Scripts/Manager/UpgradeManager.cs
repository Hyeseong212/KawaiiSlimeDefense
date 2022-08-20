using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UpgradeData
{
    public int commonUpgradeCount;
    public int rareUpgradeCount;
    public int uniqueUpgradeCount;
    public int legendaryUpgradeCount;
}
public class UpgradeManager : MonoSingleton<UpgradeManager> 
{
    [SerializeField] UpgradeData upgradeData;
    public const int commonUpgradeSpendGold = 10;
    public const float commonUpgradeAttackpts = 3;
    public const float commonUpgradeAttackspeed = 0.05f;
    /// <summary>
    /// 일반타입 업그레이드 상수
    /// </summary>
    public const int rareUpgradeSpendGold = 20;
    public const float rareUpgradeAttackpts = 20;
    public const float rareUpgradeAttackspeed = 0.1f;
    /// <summary>
    /// 레어타입 업그레이드 상수
    /// </summary>
    public const int uniqueUpgradeSpendGold = 60;
    public const float uniqueUpgradeAttackpts = 100;
    public const float uniqueUpgradeAttackspeed = 0.3f;
    /// <summary>
    /// 유니크타입 업그레이드 상수
    /// </summary>
    public const int legendaryUpgradeSpendGold = 100;
    public const float legnedaryUpgradeAttackpts = 250;
    public const float legnedaryUpgradeAttackspeed = 0.5f;
    /// <summary>
    /// 전설타입 업그레이드 상수
    /// </summary>
    
    public void CommonLevelUp()
    {
        if (upgradeData.commonUpgradeCount < 5)
        {
            if (GameManager.i.currentGold >= commonUpgradeSpendGold)
            {
                GameManager.i.currentGold -= commonUpgradeSpendGold;
                GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
                for (int i = 0; i < SlimeDataController.i.slimeDataBaseList.Count; i++)
                {
                    if (SlimeDataController.i.slimeDataBaseList[i].Type == "Common")
                    {
                        SlimeDataController.i.slimeDataBaseList[i] =
                             new SlimeData(
                             SlimeDataController.i.slimeDataBaseList[i].Index,
                             SlimeDataController.i.slimeDataBaseList[i].Name,
                             SlimeDataController.i.slimeDataBaseList[i].Type,
                             SlimeDataController.i.slimeDataBaseList[i].attackType,
                             SlimeDataController.i.slimeDataBaseList[i].attackpts + commonUpgradeAttackpts,
                             SlimeDataController.i.slimeDataBaseList[i].attackspeed + commonUpgradeAttackspeed,
                             SlimeDataController.i.slimeDataBaseList[i].Slime
                             );
                    }
                }///전체 슬라임 데이터 베이스 업그레이드
                for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
                {
                    if (CraftManager.i.currentSceneSlimeData[i].Type == "Common")
                        CraftManager.i.currentSceneSlimeData[i] =
                           new SlimeData(
                           CraftManager.i.currentSceneSlimeData[i].Index,
                           CraftManager.i.currentSceneSlimeData[i].Name,
                           CraftManager.i.currentSceneSlimeData[i].Type,
                           CraftManager.i.currentSceneSlimeData[i].Slime,
                           CraftManager.i.currentSceneSlimeData[i].attackType,
                           CraftManager.i.currentSceneSlimeData[i].attackpts + commonUpgradeAttackpts,
                           CraftManager.i.currentSceneSlimeData[i].attackspeed + commonUpgradeAttackspeed
                           );
                }///현재씬 슬라임 업그레이드
                for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
                {
                    CraftManager.i.currentSceneSlimeData[i].Slime.GetComponent<UnitController>().SetupData(CraftManager.i.currentSceneSlimeData[i]);
                }
                upgradeData.commonUpgradeCount++;
            }
            else
            {
                UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
            }
        }
        else return;
    }
    public void RareLevelUp()
    {
        if (upgradeData.rareUpgradeCount < 10 )
        {
            if(GameManager.i.currentGold >= rareUpgradeSpendGold)
            { 
                GameManager.i.currentGold -= rareUpgradeSpendGold;
                GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
                for (int i = 0; i < SlimeDataController.i.slimeDataBaseList.Count; i++)
                {
                    if (SlimeDataController.i.slimeDataBaseList[i].Type == "Rare")
                    {
                        SlimeDataController.i.slimeDataBaseList[i] =
                             new SlimeData(
                             SlimeDataController.i.slimeDataBaseList[i].Index,
                             SlimeDataController.i.slimeDataBaseList[i].Name,
                             SlimeDataController.i.slimeDataBaseList[i].Type,
                             SlimeDataController.i.slimeDataBaseList[i].attackType,
                             SlimeDataController.i.slimeDataBaseList[i].attackpts + rareUpgradeAttackpts,
                             SlimeDataController.i.slimeDataBaseList[i].attackspeed + rareUpgradeAttackspeed,
                             SlimeDataController.i.slimeDataBaseList[i].Slime
                             );
                    }
                }///전체 슬라임 데이터 베이스 업그레이드
                for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
                {
                    if (CraftManager.i.currentSceneSlimeData[i].Type == "Rare")
                        CraftManager.i.currentSceneSlimeData[i] =
                           new SlimeData(
                           CraftManager.i.currentSceneSlimeData[i].Index,
                           CraftManager.i.currentSceneSlimeData[i].Name,
                           CraftManager.i.currentSceneSlimeData[i].Type,
                           CraftManager.i.currentSceneSlimeData[i].Slime,
                           CraftManager.i.currentSceneSlimeData[i].attackType,
                           CraftManager.i.currentSceneSlimeData[i].attackpts + rareUpgradeAttackpts,
                           CraftManager.i.currentSceneSlimeData[i].attackspeed + rareUpgradeAttackspeed
                           );
                }///현재씬 슬라임 업그레이드
                upgradeData.rareUpgradeCount++;
            }
            else
            {
                UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
            }
        }
        else return;
    }
    public void UniqueLevelUp()
    {
        if (upgradeData.uniqueUpgradeCount < 15 )
        {
            if (GameManager.i.currentGold >= uniqueUpgradeSpendGold)
            {
                GameManager.i.currentGold -= uniqueUpgradeSpendGold;
                GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
                for (int i = 0; i < SlimeDataController.i.slimeDataBaseList.Count; i++)
                {
                    if (SlimeDataController.i.slimeDataBaseList[i].Type == "Unique")
                    {
                        SlimeDataController.i.slimeDataBaseList[i] =
                             new SlimeData(
                             SlimeDataController.i.slimeDataBaseList[i].Index,
                             SlimeDataController.i.slimeDataBaseList[i].Name,
                             SlimeDataController.i.slimeDataBaseList[i].Type,
                             SlimeDataController.i.slimeDataBaseList[i].attackType,
                             SlimeDataController.i.slimeDataBaseList[i].attackpts + uniqueUpgradeAttackpts,
                             SlimeDataController.i.slimeDataBaseList[i].attackspeed + uniqueUpgradeAttackspeed,
                             SlimeDataController.i.slimeDataBaseList[i].Slime
                             );
                    }
                }///전체 슬라임 데이터 베이스 업그레이드
                for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
                {
                    if (CraftManager.i.currentSceneSlimeData[i].Type == "Unique")
                        CraftManager.i.currentSceneSlimeData[i] =
                           new SlimeData(
                           CraftManager.i.currentSceneSlimeData[i].Index,
                           CraftManager.i.currentSceneSlimeData[i].Name,
                           CraftManager.i.currentSceneSlimeData[i].Type,
                           CraftManager.i.currentSceneSlimeData[i].Slime,
                           CraftManager.i.currentSceneSlimeData[i].attackType,
                           CraftManager.i.currentSceneSlimeData[i].attackpts + uniqueUpgradeAttackpts,
                           CraftManager.i.currentSceneSlimeData[i].attackspeed + uniqueUpgradeAttackspeed
                           );
                }///현재씬 슬라임 업그레이드

                upgradeData.uniqueUpgradeCount++;
            }
            else
            {
                UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
            }
        }
        else return;
    }
    public void LegendaryLevelUp()
    {
        if (upgradeData.legendaryUpgradeCount < 20)
        {
            if (GameManager.i.currentGold >= legendaryUpgradeSpendGold) 
            {
                GameManager.i.currentGold -= uniqueUpgradeSpendGold;
                GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
                for (int i = 0; i < SlimeDataController.i.slimeDataBaseList.Count; i++)
                {
                    if (SlimeDataController.i.slimeDataBaseList[i].Type == "Legendary")
                    {
                        SlimeDataController.i.slimeDataBaseList[i] =
                             new SlimeData(
                             SlimeDataController.i.slimeDataBaseList[i].Index,
                             SlimeDataController.i.slimeDataBaseList[i].Name,
                             SlimeDataController.i.slimeDataBaseList[i].Type,
                             SlimeDataController.i.slimeDataBaseList[i].attackType,
                             SlimeDataController.i.slimeDataBaseList[i].attackpts + legnedaryUpgradeAttackpts,
                             SlimeDataController.i.slimeDataBaseList[i].attackspeed + legnedaryUpgradeAttackspeed,
                             SlimeDataController.i.slimeDataBaseList[i].Slime
                             );
                    }
                }///전체 슬라임 데이터 베이스 업그레이드
                for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
                {
                    if (CraftManager.i.currentSceneSlimeData[i].Type == "Legendary")
                        CraftManager.i.currentSceneSlimeData[i] =
                           new SlimeData(
                           CraftManager.i.currentSceneSlimeData[i].Index,
                           CraftManager.i.currentSceneSlimeData[i].Name,
                           CraftManager.i.currentSceneSlimeData[i].Type,
                           CraftManager.i.currentSceneSlimeData[i].Slime,
                           CraftManager.i.currentSceneSlimeData[i].attackType,
                           CraftManager.i.currentSceneSlimeData[i].attackpts + legnedaryUpgradeAttackpts,
                           CraftManager.i.currentSceneSlimeData[i].attackspeed + legnedaryUpgradeAttackspeed
                           );
                }///현재씬 슬라임 업그레이드
                upgradeData.legendaryUpgradeCount++; 
            }
            else
            {
                UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
            }
        }
        else return;
    }
}
