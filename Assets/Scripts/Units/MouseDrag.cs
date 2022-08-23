using UnityEngine;

public class MouseDrag: MonoBehaviour
{
	[SerializeField]
	private	RectTransform		dragRectangle;			// ���콺�� �巡���� ������ ����ȭ�ϴ� Image UI�� RectTransform

	private	Rect				dragRect;				// ���콺�� �巡�� �� ���� (xMin~xMax, yMin~yMax)
	private	Vector2				start = Vector2.zero;	// �巡�� ���� ��ġ
	private	Vector2				end = Vector2.zero;		// �巡�� ���� ��ġ
	
	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;

	private void Awake()
	{
		mainCamera			= Camera.main;
		rtsUnitController	= GetComponent<RTSUnitController>();
		
		// start, end�� (0, 0)�� ���·� �̹����� ũ�⸦ (0, 0)���� ������ ȭ�鿡 ������ �ʵ��� ��
		DrawDragRectangle();
	}

	private void Update()
	{
		float wheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (mainCamera.fieldOfView > 30)
		{
			if (wheelInput > 0)
			{

				// ���� �о� ������ ���� ó�� ��
				mainCamera.fieldOfView--;
			}
		}
		if (mainCamera.fieldOfView < 90)
		{
			if (wheelInput < 0)
			{
				// ���� ��� �÷��� ���� ó�� ��
				mainCamera.fieldOfView++;
			}
		}
		if(mainCamera.fieldOfView >= 90)
        {
			mainCamera.fieldOfView = 89;
		}
		if (mainCamera.fieldOfView <= 30)
		{
			mainCamera.fieldOfView = 31;
		}
		if ( Input.GetMouseButtonDown(0) )
		{
			start	 = Input.mousePosition;
			dragRect = new Rect();
		}
		
		if ( Input.GetMouseButton(0) )
		{
			end = Input.mousePosition;
			
			// ���콺�� Ŭ���� ���·� �巡�� �ϴ� ���� �巡�� ������ �̹����� ǥ��
			DrawDragRectangle();
		}

		if ( Input.GetMouseButtonUp(0) )
		{
			// ���콺 Ŭ���� ������ �� �巡�� ���� ���� �ִ� ���� ����
			CalculateDragRect();
			SelectUnits();

			// ���콺 Ŭ���� ������ �� �巡�� ������ ������ �ʵ���
			// start, end ��ġ�� (0, 0)���� �����ϰ� �巡�� ������ �׸���
			start = end = Vector2.zero;
			DrawDragRectangle();
			BottomPanelController.i.SetSelectedSlimeImage();
			UnitControllerPanel.i.UnitSelected();
		}
	}

	private void DrawDragRectangle()
	{
		// �巡�� ������ ��Ÿ���� Image UI�� ��ġ
		dragRectangle.position	= (start + end) * 0.5f;
		// �巡�� ������ ��Ÿ���� Image UI�� ũ��
		dragRectangle.sizeDelta	= new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
	}

	private void CalculateDragRect()
	{
		if ( Input.mousePosition.x < start.x )
		{
			dragRect.xMin = Input.mousePosition.x;
			dragRect.xMax = start.x;
		}
		else
		{
			dragRect.xMin = start.x;
			dragRect.xMax = Input.mousePosition.x;
		}

		if ( Input.mousePosition.y < start.y )
		{
			dragRect.yMin = Input.mousePosition.y;
			dragRect.yMax = start.y;
		}
		else
		{
			dragRect.yMin = start.y;
			dragRect.yMax = Input.mousePosition.y;
		}
	}

	private void SelectUnits()
	{
		// ��� ������ �˻�
		//foreach ( UnitController unit in rtsUnitController.UnitList )
		//{
		//	// ������ ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ�� �巡�� ���� ���� �ִ��� �˻�
		//	if ( dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)) )
		//	{
		//		rtsUnitController.DragSelectUnit(unit);
		//	}
		//}
		UnitController unit = null;
		for (int i = 0; i < CraftManager.i.currentSceneSlimeData.Count; i++)
        {
			unit = CraftManager.i.currentSceneSlimeData[i].Slime.GetComponent<UnitController>();
			if (dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
			{
				rtsUnitController.DragSelectUnit(unit);
			} 
		}
	}
}

