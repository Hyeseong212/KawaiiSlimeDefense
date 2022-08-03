using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour , IPointerClickHandler, IDragHandler
{

    Transform _thisTr;
    Vector2 mapPosition = new Vector2(300,300);
    [SerializeField] GameObject MinimapCameraPosition;
    RectTransform cameraRect;
    void Start()
    {
        _thisTr = GetComponent<Transform>();
        cameraRect = MinimapCameraPosition.GetComponent<RectTransform>();
    }
    private void Update()
    {
        Vector3 cameraPosition= Camera.main.GetComponent<Transform>().position;

        cameraRect.anchoredPosition = new Vector2(cameraPosition.x*1.3f , cameraPosition.z*1.3f);
    }
    void MapClickMethod()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
            eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
            return;
        cameraRect.anchoredPosition = localCursor;
        Camera.main.GetComponent<Transform>().position = new Vector3(localCursor.x*0.77f, 30, localCursor.y*0.77f);

        Debug.Log("LocalCursor:" + localCursor);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
            eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
            return;
        cameraRect.anchoredPosition = localCursor;
        Camera.main.GetComponent<Transform>().position = new Vector3(localCursor.x * 0.77f, 30, localCursor.y * 0.77f);

        Debug.Log("LocalCursor:" + localCursor);
    }
}
