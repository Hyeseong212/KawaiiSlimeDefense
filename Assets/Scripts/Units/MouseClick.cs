using UnityEngine;

public class MouseClick : MonoBehaviour
{
	[SerializeField]
	private	LayerMask			layerUnit;
	[SerializeField]
	private	LayerMask			layerGround;

	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;

	private void Awake()
	{
		mainCamera			= Camera.main;
		rtsUnitController	= GetComponent<RTSUnitController>();
	}

	private void Update()
	{
		// 마우스 왼쪽 클릭으로 유닛 선택 or 해제
		if ( Input.GetMouseButtonDown(0) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// 광선에 부딪히는 오브젝트가 있을 때 (=유닛을 클릭했을 때)
			if ( Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit) )
			{
				if ( hit.transform.GetComponent<UnitController>() == null ) return;

				if ( Input.GetKey(KeyCode.LeftShift) )
				{
					rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
				}
				else
				{
					Debug.Log(1);
					rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
				}
			}
			// 광선에 부딪히는 오브젝트가 없을 때
			else
			{
				if ( !Input.GetKey(KeyCode.LeftShift) )
				{
					rtsUnitController.DeselectAll();
				}
			}
		}

		// 마우스 오른쪽 클릭으로 유닛 이동
		if ( Input.GetMouseButtonDown(1) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// 유닛 오브젝트(layerUnit)를 클릭했을 때
			if ( Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround) )
			{
				rtsUnitController.MoveSelectedUnits(hit.point);
			}
		}
	}
}

