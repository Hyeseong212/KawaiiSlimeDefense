using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoSingleton<CraftManager>
{
    //[SerializeField] List<SlimeData> currentSlime;
    [SerializeField] List<SlimeData> currentSlime;
    private void Start()
    {
        SlimeCheck();
    }
    public void SlimeCheck()//���� ���� �������� ��� �ִ��� �˾ƾ���
    {
        GameObject[] currentSceneSlimes = GameObject.FindGameObjectsWithTag("Slime");
        currentSlime = SlimeDataController.i.SlimeDataObjectFinder(currentSceneSlimes);
    }
    public void SlimeCraft()
    {

    }
}