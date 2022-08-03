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
    [SerializeField] RTSUnitController _unitController;
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
          eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
                return;
            cameraRect.anchoredPosition = localCursor;
            Camera.main.GetComponent<Transform>().position = new Vector3(localCursor.x * 0.77f, GlobalOptions.i.options.cameraYvalue, localCursor.y * 0.77f + 4);
        }
        else
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
   eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
                return;
            _unitController.MoveSelectedUnits(new Vector3(localCursor.x * 0.77f, 0, localCursor.y * 0.77f+10));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
           eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
                return;
            cameraRect.anchoredPosition = localCursor;
            Camera.main.GetComponent<Transform>().position = new Vector3(localCursor.x * 0.77f, GlobalOptions.i.options.cameraYvalue, localCursor.y * 0.77f + 4);
        }
        else
        {
            return;
        }
    }
}
