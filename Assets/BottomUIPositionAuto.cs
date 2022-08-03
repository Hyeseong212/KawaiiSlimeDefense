using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomUIPositionAuto : MonoBehaviour
{
    [SerializeField]
    GameObject StatusPanel;
    [SerializeField]
    GameObject MiniMap;
    void Start()
    {
        StatusPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 150, 0);
        MiniMap.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 210, 0);
        Debug.Log(MiniMap.GetComponent<RectTransform>().anchoredPosition);
    }
}
