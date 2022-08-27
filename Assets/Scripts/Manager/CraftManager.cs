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
        RTSUnitController.i.DeselectAll();
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
                Destroy(greenSlime);
                Destroy(greenSlime2);
                Destroy(blueSlime);
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
    }//RareSlime_Soldier_Green 크래프팅 초록 + 초록 + 파랑
    public void RareSlime_Soldier_RedSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int redCount = 0;

        bool redOk = false;
        bool greyOk = false;
        GameObject redSlime = null;
        GameObject redSlime2 = null;
        GameObject greySlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Red(Clone)")
            {
                redCount++;
                if (redCount == 1)
                {
                    redSlime = currentSceneSlimeData[i].Slime;
                }
                else if (redCount == 2)
                {
                    redSlime2 = currentSceneSlimeData[i].Slime;
                    redOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Grey(Clone)")
            {
                greySlime = currentSceneSlimeData[i].Slime;
                greyOk = true;
                break;
            }
        }
        if (redOk && greyOk)
        {
            if (redSlime != null && redSlime2 != null && greySlime != null)
            {
                Destroy(redSlime);
                Destroy(redSlime2);
                Destroy(greySlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[6].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (redCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("빨강슬라임이 2개 모자릅니다");
        }
        else if (redCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("빨강슬라임이 한개 모자릅니다");
        }
        if (!greyOk)
        {
            AlertScroll.i.CraftSlimeMsg("회색슬라임이 한개 모자릅니다");
        }
        SlimeCheck();
    }//RareSlime_Soldier_Green 크래프팅 빨강 + 빨강 + 회색
    public void RareSlime_Viking_YellowSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int yellowCount = 0;

        bool yellowOk = false;
        bool blueOk = false;
        GameObject yellowSlime = null;
        GameObject yellowSlime2 = null;
        GameObject blueSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Yellow(Clone)")
            {
                yellowCount++;
                if (yellowCount == 1)
                {
                    yellowSlime = currentSceneSlimeData[i].Slime;
                }
                else if (yellowCount == 2)
                {
                    yellowSlime2 = currentSceneSlimeData[i].Slime;
                    yellowOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Green(Clone)")//다른 포문으로 돌려보자 break걸기위함
            {
                blueSlime = currentSceneSlimeData[i].Slime;
                blueOk = true;
                break;
            }
        }
        if (blueOk && yellowOk)
        {
            if (yellowSlime != null && yellowSlime2 != null && blueSlime != null)
            {
                Destroy(yellowSlime);
                Destroy(yellowSlime2);
                Destroy(blueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[7].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (yellowCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("노랑슬라임이 2개 모자릅니다");
        }
        else if (yellowCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("노랑슬라임이 한개 모자릅니다");
        }
        if (!blueOk)
        {
            AlertScroll.i.CraftSlimeMsg("초록슬라임이 한개 모자릅니다");
        }
        SlimeCheck();
    }//RareSlime_Viking_Yellow 크래프팅 노랑 + 노랑 + 파랑
    public void RareSlime_Viking_GreySlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int greyCount = 0;

        bool greyOk = false;
        bool blueOk = false;
        GameObject greySlime = null;
        GameObject greySlime2 = null;
        GameObject blueSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Grey(Clone)")
            {
                greyCount++;
                if (greyCount == 1)
                {
                    greySlime = currentSceneSlimeData[i].Slime;
                }
                else if (greyCount == 2)
                {
                    greySlime2 = currentSceneSlimeData[i].Slime;
                    greyOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Red(Clone)")//다른 포문으로 돌려보자 break걸기위함
            {
                blueSlime = currentSceneSlimeData[i].Slime;
                blueOk = true;
                break;
            }
        }
        if (greyOk && blueOk)
        {
            if (greySlime != null && greySlime2 != null && blueSlime != null)
            {
                Destroy(greySlime);
                Destroy(greySlime2);
                Destroy(blueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[8].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (greyCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("회색슬라임이 2개 모자릅니다");
        }
        else if (greyCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("회색슬라임이 한개 모자릅니다");
        }
        if (!blueOk)
        {
            AlertScroll.i.CraftSlimeMsg("파랑슬라임이 한개 모자릅니다");
        }
        SlimeCheck();
    }//RareSlime_Viking_Yellow 크래프팅 회색 + 회색 + 파랑
}