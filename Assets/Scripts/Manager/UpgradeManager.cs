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
    public const float commonUpgradeAttackpts = 2;
    public const float commonUpgradeAttackspeed = 0.05f;
    /// <summary>
    /// 일반타입 업그레이드 상수
    /// </summary>
    public const float rareUpgradeAttackpts = 10;
    public const float rareUpgradeAttackspeed = 0.1f;
    /// <summary>
    /// 레어타입 업그레이드 상수
    /// </summary>
    public const float uniqueUpgradeAttackpts = 50;
    public const float uniqueUpgradeAttackspeed = 0.3f;
    /// <summary>
    /// 유니크타입 업그레이드 상수
    /// </summary>
    public const float legnedaryUpgradeAttackpts = 200;
    public const float legnedaryUpgradeAttackspeed = 0.5f;
    /// <summary>
    /// 전설타입 업그레이드 상수
    /// </summary>
    
    public void CommonLevelUp()
    {
        for (int i = 0; i < SlimeDataController.i.slimeDataBaseList.Count; i++) 
        {
            if(SlimeDataController.i.slimeDataBaseList[i].Type == "Common")
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
            if(CraftManager.i.currentSceneSlimeData[i].Type == "Common")
            CraftManager.i.currentSceneSlimeData[i] =
               new SlimeData(
               CraftManager.i.currentSceneSlimeData[i].Index,
               CraftManager.i.currentSceneSlimeData[i].Name,
               CraftManager.i.currentSceneSlimeData[i].Type,
               CraftManager.i.currentSceneSlimeData[i].Slime,
               CraftManager.i.currentSceneSlimeData[i].attackType,
               CraftManager.i.currentSceneSlimeData[i].attackpts + commonUpgradeAttackpts,
               CraftManager.i.currentSceneSlimeData[i].attackspeed + commonUpgradeAttackspeed,
               CraftManager.i.currentSceneSlimeData[i].SlimeMiniMapPos
               );
        }///현재씬 슬라임 업그레이드
        for(int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
        {
            CraftManager.i.currentSceneSlimeData[i].Slime.GetComponent<UnitController>().SetupData(CraftManager.i.currentSceneSlimeData[i]);
        }
        upgradeData.commonUpgradeCount ++;
    }
    public void RareLevelUp()
    {
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
                   CraftManager.i.currentSceneSlimeData[i].attackspeed + rareUpgradeAttackspeed,
                   CraftManager.i.currentSceneSlimeData[i].SlimeMiniMapPos
                   );
        }///현재씬 슬라임 업그레이드
        upgradeData.rareUpgradeCount++;
    }
    public void UniqueLevelUp()
    {
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
                   CraftManager.i.currentSceneSlimeData[i].attackspeed + uniqueUpgradeAttackspeed,
                   CraftManager.i.currentSceneSlimeData[i].SlimeMiniMapPos
                   );
        }///현재씬 슬라임 업그레이드
        upgradeData.uniqueUpgradeCount++;
    }
    public void LegendaryLevelUp()
    {
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
                   CraftManager.i.currentSceneSlimeData[i].attackspeed + legnedaryUpgradeAttackspeed,
                   CraftManager.i.currentSceneSlimeData[i].SlimeMiniMapPos
                   );
        }///현재씬 슬라임 업그레이드
        upgradeData.legendaryUpgradeCount++;
    }
}
