using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	[SerializeField]
	private	GameObject[]	unitPrefab;
	[SerializeField]
	private GameObject slimesParent;
	[SerializeField]
	private	int			maxUnitCount;

	private	Vector2		minSize = new Vector2(-3, -3);
	private	Vector2		maxSize = new Vector2(3, 3);

    public List<UnitController> SpawnUnits()
	{
		List<UnitController> unitList = new List<UnitController>(maxUnitCount);

		for ( int i = 0; i < maxUnitCount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y));
			int randomUnit = Random.Range(0, unitPrefab.Length);

			GameObject slimes = Instantiate(unitPrefab[randomUnit], position, Quaternion.identity);
			slimes.transform.parent = slimesParent.transform;
			UnitController unit	= slimes.GetComponent<UnitController>();

			unitList.Add(unit);
		}

		return unitList;
	}
}

