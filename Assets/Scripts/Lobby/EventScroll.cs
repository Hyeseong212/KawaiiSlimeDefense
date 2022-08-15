using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    const int SIZE = 3;

    Scrollbar sBar;
    float[] pos = new float[SIZE];  //각 영웅들의 위치가 저장될 배열
    float distance = 0;             //각 영웅들의 간격
    float targetPos;

    bool isDrag = false;
    


    // Start is called before the first frame update
    void Start()
    {
        sBar = GetComponentInChildren<Scrollbar>();
        SetHeroPosition();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag) sBar.value = Mathf.Lerp(sBar.value, targetPos, 0.1f); //현재 위치에서 타겟위치로 보정
    }

    void SetHeroPosition()
    {
        distance = 1f / (SIZE - 1); //영웅들의 간격구하기

        for (int i = 0; i < SIZE; i++)
        {
            pos[i] = distance * i;      //각 영웅들의 위치를 배열에 저장
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        for (int i = 0; i < SIZE; i++)
        {
            if (sBar.value < pos[i] + distance * 0.5f && sBar.value > pos[i] - distance * 0.5f)
            {
                targetPos = pos[i];
                

            }
        }
    }
}
