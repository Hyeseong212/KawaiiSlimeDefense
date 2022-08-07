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

    public void SetSelectedSlimeImage()
    {
        for (int i = 0; i < disabledSlimeImages.Length; i++)
        {
            disabledSlimeImagesBg[i].SetActive(false);
        }
        for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++)
        {
            disabledSlimeImagesBg[i].SetActive(true);
            for (int j = 0; j < slimeImg.Length; j++)
            {
                if (slimeImg[j].name + "(Clone)" == RTSUnitController.i.selectedUnitList[i].name)
                {
                    disabledSlimeImages[i].GetComponent<Image>().sprite = slimeImg[j];
                }
            }
        }
    }
    
}
