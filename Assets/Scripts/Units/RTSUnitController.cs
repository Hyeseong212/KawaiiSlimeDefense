using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RTSUnitController : MonoSingleton<RTSUnitController>
{
	[SerializeField]
	private	UnitSpawner			 unitSpawner;
	public List<UnitController> selectedUnitList;				// �÷��̾ Ŭ�� or �巡�׷� ������ ����
	public	List<UnitController> UnitList { private set; get; } // �ʿ� �����ϴ� ��� ����
	[SerializeField] private GameObject pointClick;

	WaitForSeconds delay = new WaitForSeconds(0.25f);
	Vector3 vector;
	private void Awake()
	{
		selectedUnitList = new List<UnitController>();
		if (GameManager.i.playerNumber == PlayerNumber.Player1)
		{
			UnitList = unitSpawner.SpawnUnitsPlayer1();
		}
	}

	/// <summary>
	/// ���콺 Ŭ������ ������ ������ �� ȣ��
	/// </summary>
	public void ClickSelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ� �ִ� ��� ���� ����
		DeselectAll();

		SelectUnit(newUnit);
	}

	/// <summary>
	/// Shift+���콺 Ŭ������ ������ ������ �� ȣ��
	/// </summary>
	public void ShiftClickSelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ� �ִ� ������ ����������
		if ( selectedUnitList.Contains(newUnit) )
		{
			DeselectUnit(newUnit);
		}
		// ���ο� ������ ����������
		else
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// ���콺 �巡�׷� ������ ������ �� ȣ��
	/// </summary>
	public void DragSelectUnit(UnitController newUnit)
	{
		// ���ο� ������ ����������
		if ( !selectedUnitList.Contains(newUnit) )
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// ���õ� ��� ������ �̵��� �� ȣ��
	/// </summary>
	public void MoveSelectedUnits(Vector3 end)
	{
		vector = end;
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].MoveTo(end);
		}
		StopCoroutine("ClickAnimation");
		StartCoroutine("ClickAnimation");
	}

	/// <summary>
	/// ��� ������ ������ ������ �� ȣ��
	/// </summary>
	public void DeselectAll()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].DeselectUnit();
		}

		selectedUnitList.Clear();
	}

	/// <summary>
	/// �Ű������� �޾ƿ� newUnit ���� ����
	/// </summary>
	private void SelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
		newUnit.SelectUnit();
		// ������ ���� ������ ����Ʈ�� ����
		selectedUnitList.Add(newUnit);
	}

	/// <summary>
	/// �Ű������� �޾ƿ� newUnit ���� ���� ����
	/// </summary>
	private void DeselectUnit(UnitController newUnit)
	{
		// ������ �����Ǿ��� �� ȣ���ϴ� �޼ҵ�
		newUnit.DeselectUnit();
		// ������ ���� ������ ����Ʈ���� ����
		selectedUnitList.Remove(newUnit);
	}
	IEnumerator ClickAnimation()
	{
		pointClick.SetActive(false);
		pointClick.SetActive(true);
		pointClick.transform.position = new Vector3(vector.x, vector.y + 0.1f, vector.z);
		yield return delay;
		pointClick.SetActive(false);
	}
}
