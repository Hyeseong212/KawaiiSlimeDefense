using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoSingleton<CraftManager>
{
    public List<SlimeData> currentSceneSlimeData;
    GameObject[] currentSceneSlimes;
    private void Start()
    {
        SlimeCheck();
    }
    public void SlimeCheck()//���� ���� �������� ��� �ִ��� �˾ƾ���
    {
        currentSceneSlimes = GameObject.FindGameObjectsWithTag("Slime");
        currentSceneSlimeData = SlimeDataController.i.SlimeDataObjectFinder(currentSceneSlimes);
    }
    public void SlimeCraft()
    {

    }
}