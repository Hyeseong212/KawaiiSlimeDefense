using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class MouseClick : MonoSingleton<MouseClick>
{
	[SerializeField]
	private	LayerMask			layerUnit;
	[SerializeField]
	private	LayerMask			layerGround;

	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;

	Canvas m_canvas;
	GraphicRaycaster m_gr;
	PointerEventData m_ped;
	private void Awake()
	{
		mainCamera			= Camera.main;
		rtsUnitController	= GetComponent<RTSUnitController>();
		m_canvas = GameObject.Find("PCUI(Canvas)").GetComponent<Canvas>();
		m_gr = m_canvas.GetComponent<GraphicRaycaster>();
		m_ped = new PointerEventData(null);
	}

	private void Update()
	{
		// 마우스 왼쪽 클릭으로 유닛 선택 or 해제
		if ( Input.GetMouseButtonDown(0) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();//리팩토링때 건드려야할 코드
			m_gr.Raycast(m_ped, results);
			// 광선에 부딪히는 오브젝트가 있을 때 (=유닛을 클릭했을 때)
			if (results.Count == 1)
			{
				if (results[0].gameObject.name == "map")
				{
					return;
				}
			}
			else if (results.Count == 0)
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit))
				{
					if (hit.transform.GetComponent<UnitController>() == null) return;

					if (Input.GetKey(KeyCode.LeftShift))
					{
						rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
					}
					else
					{
						rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
					}
				}
				// 광선에 부딪히는 오브젝트가 없을 때
				else
				{
					if (!Input.GetKey(KeyCode.LeftShift))
					{
						rtsUnitController.DeselectAll();
					}
				}
			}

		}

		// 마우스 오른쪽 클릭으로 유닛 이동
		if ( Input.GetMouseButtonDown(1) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();//리팩토링때 건드려야할 코드
			m_gr.Raycast(m_ped, results);
			// 유닛 오브젝트(layerUnit)를 클릭했을 때
			if (results.Count == 1)
			{
				if (results[0].gameObject.name == "map")
				{
					return;
				}
			}
			else if(results.Count == 0)
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
				{
					rtsUnitController.MoveSelectedUnits(hit.point);
				}
			}
		}
	}
	//코드꼬여있음 MiniMap.cs파일 46번째 코루틴쪽 나중에 손볼것.

}

