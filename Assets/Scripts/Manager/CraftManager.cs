using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoSingleton<CraftManager>
{
    public List<SlimeData> currentSceneSlimeData;
    public GameObject[] currentSceneSlimes;
    [SerializeField] GameObject slimeParent;

    private Vector2 player1minSize = new Vector2(-42, 78);
    private Vector2 player1maxSize = new Vector2(-38, 82);
    private void Start()
    {
        SlimeCheck();
    }
    public void SlimeCheck()//현재 씬에 슬라임이 어떤게 있는지 알아야함
    {
        currentSceneSlimes = GameObject.FindGameObjectsWithTag("Slime");
        currentSceneSlimeData = SlimeDataController.i.SlimeDataObjectFinder(currentSceneSlimes);
    }
    public void RareSlime_Soldier_GreenSlimeCraft()
    {
        SlimeCheck();
        int GreenCount = 0;

        bool GreenOk = false;
        bool BlueOk = false;
        GameObject greenSlime = null;
        GameObject greenSlime2 = null;
        GameObject blueSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Green(Clone)")
            {
                GreenCount++;
                if (GreenCount == 1)
                {
                    greenSlime = currentSceneSlimeData[i].Slime;
                }
                else if (GreenCount == 2)
                {
                    greenSlime2 = currentSceneSlimeData[i].Slime;
                    GreenOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Blue(Clone)")//다른 포문으로 돌려보자 break걸기위함
            {
                blueSlime = currentSceneSlimeData[i].Slime;
                BlueOk = true;
                break;
            }
        }
        if (BlueOk && GreenOk)
        {
            if (greenSlime != null && greenSlime2 != null && blueSlime != null)
            {
                greenSlime.GetComponent<UnitController>().DestroyThisSlime(greenSlime);
                greenSlime2.GetComponent<UnitController>().DestroyThisSlime(greenSlime2);
                blueSlime.GetComponent<UnitController>().DestroyThisSlime(blueSlime);
                DestroyImmediate(greenSlime);
                DestroyImmediate(greenSlime2);
                DestroyImmediate(blueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[5].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (GreenCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("초록슬라임이 2개 모자릅니다");
        }
        else if (GreenCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("초록슬라임이 한개 모자릅니다");
        }
        if (!BlueOk)
        {
            AlertScroll.i.CraftSlimeMsg("파랑슬라임이 한개 모자릅니다");
        }
        SlimeCheck();
    }

}