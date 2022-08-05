using UnityEngine;
using System.Collections.Generic;

public class SlimeDataController : MonoSingleton<SlimeDataController>
{

	public List<SlimeData> slimeDataBaseList;
	public int i;
	private void Awake()
	{
		List<Dictionary<string, object>> data = CSVReader.Read("SlimeDataBase");
		SlimeData slimeData = new SlimeData();
		for (var i = 0; i < data.Count; i++)
		{
			slimeData.Index = (int)data[i]["Index"];
			slimeData.Type = (string)data[i]["Type"];
			slimeData.Name = (string)data[i]["Name"];
			GameObject slime = Resources.Load<GameObject>("Kawaii Slime/Prefabs/"+slimeData.Name);
			slimeData.Slime = slime;
			slimeDataBaseList.Add(slimeData);
		}
	}
	//public List<SlimeData> SlimeDataSetIndexFinder(int[] _index)//인덱스로 찾는 슬라임데이터
 //   {
	//	List<SlimeData> slimeDatas = new List<SlimeData>();
	//	for(int i = 0; i < slimeDataBaseList.Count ; i++)
 //       {
	//		for (int j = 0; j < _index.Length; j++)
	//		{
	//			if (slimeDataBaseList[i].Index == _index[j])
	//			{
	//				slimeDatas.Add(slimeDataBaseList[i]);
	//			}
	//		}
 //       }
	//	return slimeDatas;
	//}
	//public List<SlimeData> SlimeDataNameFinder(string[] _name)//이름으로 찾는 슬라임데이터
	//{
	//	List<SlimeData> slimeDatas = new List<SlimeData>();
	//	for (int i = 0; i < slimeDataBaseList.Count; i++)
	//	{
	//		for (int j = 0; j < _name.Length; j++)
	//		{
	//			if (slimeDataBaseList[i].Name == _name[j])
	//			{
	//				slimeDatas.Add(slimeDataBaseList[i]);
	//			}
	//		}
	//	}
	//	return slimeDatas;
	//}
	public List<SlimeData> SlimeDataObjectFinder(GameObject[] _slimeObject)//게임오브젝트로 찾는 슬라임데이터
	{
		List<SlimeData> slimeDatas = new List<SlimeData>();
		for (int i = 0; i < slimeDataBaseList.Count; i++)
		{
			for (int j = 0; j < _slimeObject.Length; j++)
			{
				if (slimeDataBaseList[i].Slime.name+"(Clone)" == _slimeObject[j].name)
				{

					slimeDatas.Add(new SlimeData(slimeDataBaseList[i].Index, slimeDataBaseList[i].Name, slimeDataBaseList[i].Type, _slimeObject[j]));
				}
			}
		}
		return slimeDatas;
	}
}

