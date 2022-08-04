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
		// ���콺 ���� Ŭ������ ���� ���� or ����
		if ( Input.GetMouseButtonDown(0) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();//�����丵�� �ǵ������ �ڵ�
			m_gr.Raycast(m_ped, results);
			// ������ �ε����� ������Ʈ�� ���� �� (=������ Ŭ������ ��)
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
				// ������ �ε����� ������Ʈ�� ���� ��
				else
				{
					if (!Input.GetKey(KeyCode.LeftShift))
					{
						rtsUnitController.DeselectAll();
					}
				}
			}

		}

		// ���콺 ������ Ŭ������ ���� �̵�
		if ( Input.GetMouseButtonDown(1) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();//�����丵�� �ǵ������ �ڵ�
			m_gr.Raycast(m_ped, results);
			// ���� ������Ʈ(layerUnit)�� Ŭ������ ��
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
	//�ڵ岿������ MiniMap.cs���� 46��° �ڷ�ƾ�� ���߿� �պ���.

}

