using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoSingleton<UnitSpawner>
{
	[SerializeField]
	private	GameObject[]	unitPrefab;
	[SerializeField]
	private GameObject slimesParent;
	[SerializeField]
	public	int			maxUnitCount;


	private	Vector2	player1minSize = new Vector2(-42, 78);
	private	Vector2 player1maxSize = new Vector2(-38, 82);

	public int Index = 0;
    public List<UnitController> SpawnUnitsPlayer1()//슬라임 스폰
	{
		List<UnitController> unitList = new List<UnitController>(maxUnitCount);

		for ( int i = 0; i < maxUnitCount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(player1minSize.x, player1maxSize.x), 1, Random.Range(player1minSize.y, player1maxSize.y));
			int randomUnit = Random.Range(9, 12);

			GameObject slimes = Instantiate(unitPrefab[randomUnit], position, Quaternion.identity);
			slimes.transform.parent = slimesParent.transform;
			AlertScroll.i.SlimeGeneSetter(slimes);
			UnitController unit	= slimes.GetComponent<UnitController>();
			unitList.Add(unit);
		}

		return unitList;
	}
}

