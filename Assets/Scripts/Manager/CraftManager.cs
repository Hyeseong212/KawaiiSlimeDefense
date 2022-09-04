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
    public void SlimeCheck()//���� ���� �������� ��� �ִ��� �˾ƾ���
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
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Blue(Clone)")//�ٸ� �������� �������� break�ɱ�����
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
            AlertScroll.i.CraftSlimeMsg("�ʷϽ������� 2�� ���ڸ��ϴ�");
        }
        else if (GreenCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("�ʷϽ������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!BlueOk)
        {
            AlertScroll.i.CraftSlimeMsg("�Ķ��������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//RareSlime_Soldier_Green ũ������ �ʷ� + �ʷ� + �Ķ�
    public void RareSlime_Soldier_RedSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int redCount = 0;

        bool redOk = false;
        bool blueOk = false;
        GameObject redSlime = null;
        GameObject redSlime2 = null;
        GameObject blueSlime = null;
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
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Blue(Clone)")
            {
                blueSlime = currentSceneSlimeData[i].Slime;
                blueOk = true;
                break;
            }
        }
        if (redOk && blueOk)
        {
            if (redSlime != null && redSlime2 != null && blueSlime != null)
            {
                Destroy(redSlime);
                Destroy(redSlime2);
                Destroy(blueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[6].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (redCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("������������ 2�� ���ڸ��ϴ�");
        }
        else if (redCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("������������ �Ѱ� ���ڸ��ϴ�");
        }
        if (!blueOk)
        {
            AlertScroll.i.CraftSlimeMsg("�Ķ��������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//RareSlime_Soldier_Green ũ������ ���� + ���� + �Ķ�
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
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Blue(Clone)")//�ٸ� �������� �������� break�ɱ�����
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
            AlertScroll.i.CraftSlimeMsg("����������� 2�� ���ڸ��ϴ�");
        }
        else if (yellowCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("����������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!blueOk)
        {
            AlertScroll.i.CraftSlimeMsg("�Ķ��������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//RareSlime_Viking_Yellow ũ������ ��� + ��� + �Ķ�
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
            if (currentSceneSlimeData[i].Slime.name == "CommonSlime_Red(Clone)")//�ٸ� �������� �������� break�ɱ�����
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
            AlertScroll.i.CraftSlimeMsg("ȸ���������� 2�� ���ڸ��ϴ�");
        }
        else if (greyCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("ȸ���������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!blueOk)
        {
            AlertScroll.i.CraftSlimeMsg("�Ķ��������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//RareSlime_Viking_Yellow ũ������ ȸ�� + ȸ�� + �Ķ�
    /// <summary>
    /// ����
    /// </summary>
    public void UniqueSlime_NekoSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();

        bool GreenRareOk = false;
        bool YellowRareOk = false;
        bool GreyRareOk = false;
        GameObject greenRareSlime = null;
        GameObject yellowRareSlime = null;
        GameObject greyRareSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Soldier_Green(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                greenRareSlime = currentSceneSlimeData[i].Slime;
                GreenRareOk = true;
                break;
            }
        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Viking_Yellow(Clone)")
            {
                yellowRareSlime = currentSceneSlimeData[i].Slime;
                YellowRareOk = true;
                break;
            }
        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Viking_Grey(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                greyRareSlime = currentSceneSlimeData[i].Slime;
                GreyRareOk = true;
                break;
            }
        }
        if (GreenRareOk && YellowRareOk && GreyRareOk)
        {
            if (greenRareSlime != null && yellowRareSlime != null && greyRareSlime != null)
            {
                Destroy(greenRareSlime);
                Destroy(yellowRareSlime);
                Destroy(greyRareSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[9].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (!GreenRareOk)
        {
            AlertScroll.i.CraftSlimeMsg("�ʷ� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        else if (!YellowRareOk)
        {
            AlertScroll.i.CraftSlimeMsg("��� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!GreyRareOk)
        {
            AlertScroll.i.CraftSlimeMsg("ȸ�� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//UniqueSlime_Neko �ʷϷ��� + ������� + ȸ������ = ���ڽ�����
    public void UniqueSlime_QueenSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int greyRareCount = 0;

        bool greyRareOk = false;
        bool yellowRareOk = false;
        GameObject greyRareSlime = null;
        GameObject greyRareSlime2 = null;
        GameObject yellowRareSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Viking_Grey(Clone)")
            {
                greyRareCount++;
                if (greyRareCount == 1)
                {
                    greyRareSlime = currentSceneSlimeData[i].Slime;
                }
                else if (greyRareCount == 2)
                {
                    greyRareSlime2 = currentSceneSlimeData[i].Slime;
                    greyRareOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Viking_Yellow(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                yellowRareSlime = currentSceneSlimeData[i].Slime;
                yellowRareOk = true;
                break;
            }
        }
        if (greyRareOk && yellowRareOk)
        {
            if (greyRareSlime != null && greyRareSlime2 != null && yellowRareSlime != null)
            {
                Destroy(greyRareSlime);
                Destroy(greyRareSlime2);
                Destroy(yellowRareSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[10].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (greyRareCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("ȸ�� ���� �������� 2�� ���ڸ��ϴ�");
        }
        else if (greyRareCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("ȸ�� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!yellowRareOk)
        {
            AlertScroll.i.CraftSlimeMsg("��� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//UniqueSlime_Queen ȸ������ + ȸ������  + ��� ���� = ��������
    public void UniqueSlime_SproutSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int redRareCount = 0;

        bool redRareOk = false;
        bool greenRareOk = false;
        GameObject redRareSlime = null;
        GameObject redRareSlime2 = null;
        GameObject greenRareSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Soldier_Red(Clone)")
            {
                redRareCount++;
                if (redRareCount == 1)
                {
                    redRareSlime = currentSceneSlimeData[i].Slime;
                }
                else if (redRareCount == 2)
                {
                    redRareSlime2 = currentSceneSlimeData[i].Slime;
                    redRareOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "RareSlime_Soldier_Green(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                greenRareSlime = currentSceneSlimeData[i].Slime;
                greenRareOk = true;
                break;
            }
        }
        if (greenRareOk && redRareOk)
        {
            if (redRareSlime != null && redRareSlime2 != null && greenRareSlime != null)
            {
                Destroy(redRareSlime);
                Destroy(redRareSlime2);
                Destroy(greenRareSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[11].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (redRareCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("���� ���� �������� 2�� ���ڸ��ϴ�");
        }
        else if (redRareCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("���� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!greenRareOk)
        {
            AlertScroll.i.CraftSlimeMsg("�ʷ� ���� �������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//UniqueSlime_Sprout �������� + �������� + �ʷϷ��� = ���Ͻ����� 
    /// <summary>
    /// ����ũ
    /// </summary>
    public void LegendarySlime_KingSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();

        bool NekoUniqueOk = false;
        bool QueenUniqueOk = false;
        bool SproutUniqueOk = false;
        GameObject NekoUniqueSlime = null;
        GameObject QueenUniqueSlime = null;
        GameObject SproutUniqueSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "UniqueSlime_Neko(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                NekoUniqueSlime = currentSceneSlimeData[i].Slime;
                NekoUniqueOk = true;
                break;
            }
        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "UniqueSlime_Queen(Clone)")
            {
                QueenUniqueSlime = currentSceneSlimeData[i].Slime;
                QueenUniqueOk = true;
                break;
            }
        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "UniqueSlime_Sprout(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                SproutUniqueSlime = currentSceneSlimeData[i].Slime;
                SproutUniqueOk = true;
                break;
            }
        }
        if (NekoUniqueOk && QueenUniqueOk && SproutUniqueOk)
        {
            if (NekoUniqueSlime != null && QueenUniqueSlime != null && SproutUniqueSlime != null)
            {
                Destroy(NekoUniqueSlime);
                Destroy(QueenUniqueSlime);
                Destroy(SproutUniqueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[12].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (!NekoUniqueOk)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� �Ѱ� ���ڸ��ϴ�");
        }
        else if (!QueenUniqueOk)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!SproutUniqueOk)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//LegendarySlime_King ���� + ���� + ���� = ŷ������
    public void LegendarySlime_RabbitSlimeCraft()
    {
        SlimeCheck();
        RTSUnitController.i.DeselectAll();
        int QueenUniqueCount = 0;

        bool QueenUniqueOk = false;
        bool SproutUniqueOk = false;
        GameObject QueenUniqueSlime = null;
        GameObject QueenUniqueSlime2 = null;
        GameObject SproutUniqueSlime = null;
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "UniqueSlime_Queen(Clone)")
            {
                QueenUniqueCount++;
                if (QueenUniqueCount == 1)
                {
                    QueenUniqueSlime = currentSceneSlimeData[i].Slime;
                }
                else if (QueenUniqueCount == 2)
                {
                    QueenUniqueSlime2 = currentSceneSlimeData[i].Slime;
                    QueenUniqueOk = true;
                    break;
                }
            }

        }
        for (int i = 0; i < currentSceneSlimeData.Count; i++)
        {
            if (currentSceneSlimeData[i].Slime.name == "UniqueSlime_Sprout(Clone)")//�ٸ� �������� �������� break�ɱ�����
            {
                SproutUniqueSlime = currentSceneSlimeData[i].Slime;
                SproutUniqueOk = true;
                break;
            }
        }
        if (QueenUniqueOk && SproutUniqueOk)
        {
            if (QueenUniqueSlime != null && QueenUniqueSlime2 != null && SproutUniqueSlime != null)
            {
                Destroy(QueenUniqueSlime);
                Destroy(QueenUniqueSlime2);
                Destroy(SproutUniqueSlime);
                Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
                GameObject slime = Instantiate(SlimeDataController.i.slimeDataBaseList[13].Slime, position, Quaternion.identity);
                slime.transform.parent = slimeParent.transform;
                Debug.Log(slime);
                AlertScroll.i.SlimeGeneSetter(slime);
            }
        }
        else if (QueenUniqueCount == 0)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� 2�� ���ڸ��ϴ�");
        }
        else if (QueenUniqueCount == 1)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� �Ѱ� ���ڸ��ϴ�");
        }
        if (!SproutUniqueOk)
        {
            AlertScroll.i.CraftSlimeMsg("���� ����ũ �������� �Ѱ� ���ڸ��ϴ�");
        }
        SlimeCheck();
    }//LegendarySlime_Rabbit ���� + ���� + ���� = �䳢������ 
    /// <summary>
    /// ��������
    /// </summary>
}