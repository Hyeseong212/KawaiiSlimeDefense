using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RTSUnitController : MonoSingleton<RTSUnitController>
{
	[SerializeField]
	private	UnitSpawner			 unitSpawner;
	public List<UnitController> selectedUnitList;				// 플레이어가 클릭 or 드래그로 선택한 유닛
	public	List<UnitController> UnitList { private set; get; } // 맵에 존재하는 모든 유닛
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
	/// 마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 모든 유닛 해제
		DeselectAll();

		SelectUnit(newUnit);
	}

	/// <summary>
	/// Shift+마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ShiftClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 유닛을 선택했으면
		if ( selectedUnitList.Contains(newUnit) )
		{
			DeselectUnit(newUnit);
		}
		// 새로운 유닛을 선택했으면
		else
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 마우스 드래그로 유닛을 선택할 때 호출
	/// </summary>
	public void DragSelectUnit(UnitController newUnit)
	{
		// 새로운 유닛을 선택했으면
		if ( !selectedUnitList.Contains(newUnit) )
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 선택된 모든 유닛을 이동할 때 호출
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
	/// 모든 유닛의 선택을 해제할 때 호출
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
	/// 매개변수로 받아온 newUnit 선택 설정
	/// </summary>
	private void SelectUnit(UnitController newUnit)
	{
		// 유닛이 선택되었을 때 호출하는 메소드
		newUnit.SelectUnit();
		// 선택한 유닛 정보를 리스트에 저장
		selectedUnitList.Add(newUnit);
	}

	/// <summary>
	/// 매개변수로 받아온 newUnit 선택 해제 설정
	/// </summary>
	private void DeselectUnit(UnitController newUnit)
	{
		// 유닛이 해제되었을 때 호출하는 메소드
		newUnit.DeselectUnit();
		// 선택한 유닛 정보를 리스트에서 삭제
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

