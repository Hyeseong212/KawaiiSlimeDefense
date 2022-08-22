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
	[SerializeField] private GameObject attackPointClick;

	WaitForSeconds delay = new WaitForSeconds(0.5f);
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
			selectedUnitList[i].GetComponentInChildren<Shooter>().status = SlimeStatus.ForcedMove;
			//if (selectedUnitList[i].GetComponentInChildren<Shooter>().status == SlimeStatus.ForcedAttack)
   //         {
   //             selectedUnitList[i].GetComponentInChildren<Shooter>().status = SlimeStatus.ForcedAttack;
   //         }
   //         else
   //         {
   //             selectedUnitList[i].GetComponentInChildren<Shooter>().status = SlimeStatus.ForcedMove;
   //         }
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
	/// <summary>
	/// �Ű������� �޾ƿ� newUnit�� ���� ���ֵ� ��μ���
	/// </summary>
	public void SeletUnitDoubleClick(UnitController newUnit)
	{
		DeselectAll();
		// �ش����� ����Ŭ���� 
		CraftManager.i.SlimeCheck();//��������ִ� ������üũ
		for(int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; ++ i)
        {
			if(newUnit.slimedata.Index == CraftManager.i.currentSceneSlimeData[i].Index)
            {
				CraftManager.i.currentSceneSlimeData[i].Slime.GetComponent<UnitController>().SelectUnit();//����Ŭ���ѽ����Ӱ� ���������� ��μ���
				selectedUnitList.Add(CraftManager.i.currentSceneSlimeData[i].Slime.GetComponent<UnitController>());
			}
		}
		// ������ ���� ������ ����Ʈ�� ����
	}
	IEnumerator ClickAnimation()
	{
		if (selectedUnitList.Count > 0)
		{
			if (selectedUnitList[0].shooter.status == SlimeStatus.ForcedMove)
			{
				pointClick.SetActive(false);
				pointClick.SetActive(true);
				pointClick.transform.position = new Vector3(vector.x, vector.y + 0.1f, vector.z);
				yield return delay;
				pointClick.SetActive(false);
			}
			else if(selectedUnitList[0].shooter.status == SlimeStatus.ForcedAttack)
            {
				attackPointClick.SetActive(false);
				attackPointClick.SetActive(true);
				attackPointClick.transform.position = new Vector3(vector.x, vector.y + 0.1f, vector.z);
				yield return delay;
				attackPointClick.SetActive(false);
			}
		}
	}

}

