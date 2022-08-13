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
	private LayerMask layerGround; 
	[SerializeField]
	private LayerMask layerEnemy;

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
			Physics.Raycast(ray,out hit);
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
						BottomPanelController.i.SetSelectedSlimeImage();
					}
					else
					{
						rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
						BottomPanelController.i.SetSelectedSlimeImage();
					}
				}
				// ������ �ε����� ������Ʈ�� ���� ��
				else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy))
				{
					for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++) 
					{
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().enemies.Clear();
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().status = SlimeStatus.ForcedAttack;
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().enemies.Insert(0,hit.collider.gameObject.GetComponent<Enemy>().thisEnemydata);
					}
				}
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

