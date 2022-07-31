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
    public void SlimeCheck()//현재 씬에 슬라임이 어떤게 있는지 알아야함
    {
        GameObject[] currentSceneSlimes = GameObject.FindGameObjectsWithTag("Slime");
        currentSlime = SlimeDataController.i.SlimeDataObjectFinder(currentSceneSlimes);
    }
    public void SlimeCraft()
    {

    }
}