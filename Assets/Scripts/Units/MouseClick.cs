using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class MouseClick : MonoSingleton<MouseClick>
{
	[SerializeField]
	private	LayerMask	layerUnit;
	[SerializeField]
	private LayerMask	layerGround;
	[SerializeField]
	private LayerMask	layerEnemy; 
	[SerializeField]
	private LayerMask	layerBuilding;

	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;

	Canvas m_canvas;
	GraphicRaycaster m_gr;
	PointerEventData m_ped;
	/// <summary>
	/// ����Ŭ������
	/// </summary>
	public float m_DoubleClickSecond = 0.25f;
	private bool m_IsOneClick = false;
	private double m_Timer = 0;
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
					BottomPanelController.i.SetDeActivative();
					UnitControllerPanel.i.BuildingDeselected();
					if (hit.transform.GetComponent<UnitController>() == null) return;

					if (Input.GetKey(KeyCode.LeftShift))
					{
						rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
						BottomPanelController.i.SetSelectedSlimeImage();
						UnitControllerPanel.i.UnitSelected();
					}
					else
					{
						if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
						{
							//�ѹ�Ŭ��
							rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
							BottomPanelController.i.SetSelectedSlimeImage();
							UnitControllerPanel.i.UnitSelected();
							m_IsOneClick = false;
						}

						if (Input.GetMouseButtonDown(0))
						{
							if (!m_IsOneClick)
							{
								m_Timer = Time.time;
								m_IsOneClick = true;
							}

							else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
							{
								//�ι�Ŭ��
								rtsUnitController.SeletUnitDoubleClick(hit.transform.GetComponent<UnitController>());
								UnitControllerPanel.i.UnitSelected();
								m_IsOneClick = false;
							}
						}

					}
				}//����Ŭ��
				else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy))
				{
					UnitControllerPanel.i.BuildingDeselected();
					UnitControllerPanel.i.UnitDeselected();
					BottomPanelController.i.SetDeActivative();
					for (int i = 0; i < RTSUnitController.i.selectedUnitList.Count; i++) 
					{
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().status = SlimeStatus.ForcedAttack;
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().targetedEnemy = hit.collider.gameObject.GetComponent<Enemy>().thisEnemydata;
						RTSUnitController.i.selectedUnitList[i].GetComponentInChildren<Shooter>().enemies.Insert(0,hit.collider.gameObject.GetComponent<Enemy>().thisEnemydata);
					}
				}//��Ŭ��
				else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerBuilding))
                {
					rtsUnitController.DeselectAll();
					UnitControllerPanel.i.UnitDeselected();
					if (GameManager.i.playerNumber+ "UpgradeBuilding" == PlayerNumber.Player1 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
                    {
						BottomPanelController.i.SetRenderBuildingActive(hit.collider.tag);
						UnitControllerPanel.i.UpgradeBuildingClicked();
					}
					else if (GameManager.i.playerNumber + "GambleBuilding" == PlayerNumber.Player1 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{
						Debug.Log(GameManager.i.playerNumber + "GambleBuilding");
					}
					else if (GameManager.i.playerNumber + "UpgradeBuilding" == PlayerNumber.Player2 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
					else if (GameManager.i.playerNumber + "GambleBuilding" == PlayerNumber.Player2 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
					else if (GameManager.i.playerNumber + "UpgradeBuilding" == PlayerNumber.Player3 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
					else if (GameManager.i.playerNumber + "GambleBuilding" == PlayerNumber.Player3 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
					else if (GameManager.i.playerNumber + "UpgradeBuilding" == PlayerNumber.Player4 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
					else if (GameManager.i.playerNumber + "GambleBuilding" == PlayerNumber.Player4 + hit.collider.name)//�÷��̾�1 ���ۺ����϶�
					{

					}
				}
                else
                {
					if (!Input.GetKey(KeyCode.LeftShift))
					{
						rtsUnitController.DeselectAll();
						BottomPanelController.i.SetDeActivative();
						UnitControllerPanel.i.BuildingDeselected(); //�ǹ� ��������
						UnitControllerPanel.i.UnitDeselected(); // ���� ��������
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

