using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelController : MonoSingleton<BottomPanelController>
{
    [SerializeField] GameObject[] disabledSlimeImages;
    [SerializeField] GameObject[] disabledSlimeImagesBg;
    [SerializeField] GameObject statusPanel;
    [SerializeField] Sprite[] slimeImg;
    [SerializeField] GameObject[] rendererSlime;
    [SerializeField] GameObject rendererBuilding;
    public void SetSelectedSlimeImage()
    {
        for (int i = 0; i < disabledSlimeImages.Length; i++)
        {
            disabledSlimeImagesBg[i].SetActive(false);
        }
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++)
        {
            if(i == 12)
            {
                break;
            }
            disabledSlimeImagesBg[i].SetActive(true);
            for (int j = 0; j < slimeImg.Length; j++)
            {
                if (slimeImg[j].name + "(Clone)" == RTSUnitController.i.selectedUnitList[i].name)
                {
                    disabledSlimeImages[i].GetComponent<Image>().sprite = slimeImg[j];
                }
            }
        }
        if(RTSUnitController.i.selectedUnitList.Count > 0) 
        { 
            SetModelPos(RTSUnitController.i.selectedUnitList[0].name);//리스트중에 처음있는놈만 렌더러 텍스쳐에 비춰줌
        }
        else
        {
            for (int i = 0; i < rendererSlime.Length; i++)
            {
                rendererSlime[i].gameObject.SetActive(false);
            }
        }
    }
    void SetModelPos(string slimeName)
    {
        for (int i = 0; i < rendererSlime.Length; i++)
        {
            if (slimeName == rendererSlime[i].name + "(Clone)")
            {
                rendererSlime[i].gameObject.SetActive(true);
                //rendererSlime[i].gameObject.GetComponent<RenderTextureSlime>().StopCoroutine("RandomCoroutine");
                //rendererSlime[i].gameObject.GetComponent<RenderTextureSlime>().StartCoroutine("RandomCoroutine");
            }
            else
            {
                rendererSlime[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetRenderBuildingActive(string building)
    {
        if (building == "Building") 
        {
            rendererBuilding.SetActive(true); 
        }
    }
    public void SetDeActivative()
    {
        rendererBuilding.SetActive(false);
    }
}
