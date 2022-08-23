using UnityEngine;

public class MouseDrag: MonoBehaviour
{
	[SerializeField]
	private	RectTransform		dragRectangle;			// 마우스로 드래그한 범위를 가시화하는 Image UI의 RectTransform

	private	Rect				dragRect;				// 마우스로 드래그 한 범위 (xMin~xMax, yMin~yMax)
	private	Vector2				start = Vector2.zero;	// 드래그 시작 위치
	private	Vector2				end = Vector2.zero;		// 드래그 종료 위치
	
	private	Camera				mainCamera;
	private	RTSUnitController	rtsUnitController;

	private void Awake()
	{
		mainCamera			= Camera.main;
		rtsUnitController	= GetComponent<RTSUnitController>();
		
		// start, end가 (0, 0)인 상태로 이미지의 크기를 (0, 0)으로 설정해 화면에 보이지 않도록 함
		DrawDragRectangle();
	}

	private void Update()
	{
		float wheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (mainCamera.fieldOfView > 30)
		{
			if (wheelInput > 0)
			{

				// 휠을 밀어 돌렸을 때의 처리 ↑
				mainCamera.fieldOfView--;
			}
		}
		if (mainCamera.fieldOfView < 90)
		{
			if (wheelInput < 0)
			{
				// 휠을 당겨 올렸을 때의 처리 ↓
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
			
			// 마우스를 클릭한 상태로 드래그 하는 동안 드래그 범위를 이미지로 표현
			DrawDragRectangle();
		}

		if ( Input.GetMouseButtonUp(0) )
		{
			// 마우스 클릭을 종료할 때 드래그 범위 내에 있는 유닛 선택
			CalculateDragRect();
			SelectUnits();

			// 마우스 클릭을 종료할 때 드래그 범위가 보이지 않도록
			// start, end 위치를 (0, 0)으로 설정하고 드래그 범위를 그린다
			start = end = Vector2.zero;
			DrawDragRectangle();
			BottomPanelController.i.SetSelectedSlimeImage();
			UnitControllerPanel.i.UnitSelected();
		}
	}

	private void DrawDragRectangle()
	{
		// 드래그 범위를 나타내는 Image UI의 위치
		dragRectangle.position	= (start + end) * 0.5f;
		// 드래그 범위를 나타내는 Image UI의 크기
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
		// 모든 유닛을 검사
		//foreach ( UnitController unit in rtsUnitController.UnitList )
		//{
		//	// 유닛의 월드 좌표를 화면 좌표로 변환해 드래그 범위 내에 있는지 검사
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

