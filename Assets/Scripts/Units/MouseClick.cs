using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour
{
	[SerializeField]
	private	LayerMask			layerUnit;
	[SerializeField]
	private	LayerMask			layerGround;

	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;
	[SerializeField] private GameObject pointClick;

	WaitForSeconds delay = new WaitForSeconds(0.25f);
	private void Awake()
	{
		mainCamera			= Camera.main;
		rtsUnitController	= GetComponent<RTSUnitController>();
	}

	private void Update()
	{
		// ���콺 ���� Ŭ������ ���� ���� or ����
		if ( Input.GetMouseButtonDown(0) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// ������ �ε����� ������Ʈ�� ���� �� (=������ Ŭ������ ��)
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
			// ������ �ε����� ������Ʈ�� ���� ��
			else
			{
				if ( !Input.GetKey(KeyCode.LeftShift) )
				{
					rtsUnitController.DeselectAll();
				}
			}
		}

		// ���콺 ������ Ŭ������ ���� �̵�
		if ( Input.GetMouseButtonDown(1) )
		{
			RaycastHit	hit;
			Ray			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// ���� ������Ʈ(layerUnit)�� Ŭ������ ��
			if ( Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround) )
			{
				rtsUnitController.MoveSelectedUnits(hit.point);
				StartCoroutine(ClickAnimation(hit.point));
			}
		}
	}
	IEnumerator ClickAnimation(Vector3 hit)
    {
		pointClick.SetActive(true);
		pointClick.transform.position = new Vector3(hit.x,hit.y+0.1f,hit.z);
		yield return delay;
		pointClick.SetActive(false);
	}
}

