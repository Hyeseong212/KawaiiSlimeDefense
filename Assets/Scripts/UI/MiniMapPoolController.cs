using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPoolController : MonoSingleton<MiniMapPoolController>
{
    [SerializeField] GameObject slimePool;
    [SerializeField] GameObject enemyPool;
    [SerializeField] GameObject buildingPool;

    [SerializeField] GameObject slimeMiniMapPrefab;
    [SerializeField] GameObject enemyMiniMapPrefab;
    [SerializeField] GameObject BuildingMiniMapPrefab;


    RectTransform slimeRectTransform;
    List<RectTransform> slimeRectTransformList;
    private void Start()
    {
        slimeRectTransformList = new List<RectTransform>();
    }
    private void Update()
    {
        if (slimeRectTransformList != null) 
        {
            for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
            {
                slimeRectTransformList[i].anchoredPosition = new Vector2(CraftManager.i.currentSceneSlimeData[i].Slime.transform.position.x * 1.3f, CraftManager.i.currentSceneSlimeData[i].Slime.transform.position.z * 1.3f);
                CraftManager.i.currentSceneSlimeData[i] =
                   new SlimeData(
                   CraftManager.i.currentSceneSlimeData[i].Index,
                   CraftManager.i.currentSceneSlimeData[i].Name,
                   CraftManager.i.currentSceneSlimeData[i].Type,
                   CraftManager.i.currentSceneSlimeData[i].Slime,
                   CraftManager.i.currentSceneSlimeData[i].attackType,
                   CraftManager.i.currentSceneSlimeData[i].attackpts,
                   CraftManager.i.currentSceneSlimeData[i].attackspeed,
                   CraftManager.i.currentSceneSlimeData[i].SlimeMiniMapPos
                   );
            } 
        }
    }
    public void MiniMapSlimeImageSetter()
    {
        for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++) 
        {
            Vector3 slimePos = CraftManager.i.currentSceneSlimeData[i].Slime.transform.position * 1.3f;

            GameObject miniMapSlime = Instantiate(slimeMiniMapPrefab,new Vector2 (slimePos.x, slimePos.z), Quaternion.identity);
            CraftManager.i.currentSceneSlimeData[i] =
                new SlimeData(
                CraftManager.i.currentSceneSlimeData[i].Index,
                CraftManager.i.currentSceneSlimeData[i].Name,
                CraftManager.i.currentSceneSlimeData[i].Type,
                CraftManager.i.currentSceneSlimeData[i].Slime,
                CraftManager.i.currentSceneSlimeData[i].attackType,
                CraftManager.i.currentSceneSlimeData[i].attackpts,
                CraftManager.i.currentSceneSlimeData[i].attackspeed,
                miniMapSlime
                );
            slimeRectTransform = miniMapSlime.GetComponent<RectTransform>();
            slimeRectTransform.anchoredPosition = new Vector2(slimePos.x, slimePos.z);
            slimeRectTransformList.Add(slimeRectTransform);
            slimeRectTransform.SetParent(slimePool.transform);

        }
    }
    public void MiniMapslimeImageDestroyer()
    {

    }
    //¿ÀºêÁ§Æ® ÆÄ±«½Ã ÀÌ°Íµµ ÆÄ±«µÅ¾ßÇÔ
}
