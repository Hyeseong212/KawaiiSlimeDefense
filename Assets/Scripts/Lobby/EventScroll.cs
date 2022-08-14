using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    const int SIZE = 3;

    Scrollbar sBar;
    float[] pos = new float[SIZE];  //�� �������� ��ġ�� ����� �迭
    float distance = 0;             //�� �������� ����
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
        if (!isDrag) sBar.value = Mathf.Lerp(sBar.value, targetPos, 0.1f); //���� ��ġ���� Ÿ����ġ�� ����
    }

    void SetHeroPosition()
    {
        distance = 1f / (SIZE - 1); //�������� ���ݱ��ϱ�

        for (int i = 0; i < SIZE; i++)
        {
            pos[i] = distance * i;      //�� �������� ��ġ�� �迭�� ����
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
