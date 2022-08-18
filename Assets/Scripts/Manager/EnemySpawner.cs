using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EnemyData
{
    public EnemyData(int _wave, int _index, string _type, string _name, float _speed, float _hp)
    {
        this.wave = _wave;
        this.index = _index;
        this.type = _type;
        this.name = _name;
        this.speed = _speed;
        this.hp = _hp;
        this.enemyObject = null;
        this.pos = Vector3.zero;
    }
    public EnemyData(int _wave, int _index, string _type,string _name, float _speed, float _hp, GameObject _enemyObject)
    {
        this.wave = _wave;
        this.index = _index;
        this.type = _type;
        this.name = _name;
        this.speed = _speed;
        this.hp = _hp;
        this.enemyObject = _enemyObject;
        this.pos = _enemyObject.GetComponent<Transform>().position;
    }
    public int wave,index;
    public string type,name;
    public float speed,hp;
    public GameObject enemyObject;
    public Vector3 pos;

}
public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField]
    private GameObject enemiesParent;
    private GameObject enemyPrefab; // 적 프리팹
    [SerializeField]
    private float spawnTime; //적 생성 주기
    [SerializeField]
    public Transform[] P1wayPoints; //Player 1의 이동경로
    [SerializeField]
    public Transform[] P2wayPoints; //Player 2의 이동경로
    [SerializeField]
    public Transform[] P3wayPoints; //Player 3의 이동경로
    [SerializeField]
    public Transform[] P4wayPoints; //Player 4의 이동경로

    int monsterCount=0;
    int MaxCount;
    public int TestMaxCount;
    public List<EnemyData> enemyDataBaseList; //모든적의 정보
    public List<EnemyData> enemyInThisWaveList; //모든적의 정보
    //웨이브 정보
    public int currentWave = 0;

    // 적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요없다
    private void Awake()
    {
        //적리스트 메모리 할당
        EnemyData enemyData = new EnemyData();
        enemyDataBaseList = new List<EnemyData>();
        //적생성 코루틴 함수 호출
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyDataBase");
        for (var i = 0; i < data.Count; i++)
        {
            enemyData.wave = (int)data[i]["Wave"];
            enemyData.index = (int)data[i]["Index"];
            enemyData.type = (string)data[i]["Type"];
            enemyData.name = (string)data[i]["Name"];
            enemyData.speed = Convert.ToSingle(data[i]["Speed"]);
            enemyData.hp = Convert.ToSingle(data[i]["HP"]);
            GameObject enemy = Resources.Load<GameObject>("NewPrefabs/Enemies/" + enemyData.name);
            enemyData.enemyObject = enemy;
            enemyData.pos = Vector3.zero;
            enemyDataBaseList.Add(enemyData);
        }
    }
    public void CoroutineTrggier()
    {
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy() 
    {
        while (monsterCount != MaxCount)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, P1wayPoints[0].transform.position, Quaternion.identity); // 적 오브젝트 생성
            Enemy enemy = enemyObject.GetComponent<Enemy>(); // 방금 생성된 적의 Enmey 컴포넌트
            enemyObject.transform.parent = enemiesParent.transform;
            EnemyData enemydata = new EnemyData();
            for (int i = 0; i < enemyDataBaseList.Count; i++) //에너미 리스트에서 이름같은 에너미정보 가져와서 해당 에너미 정보에 에너미 정보 셋업
            {
                if (enemyObject.name == enemyDataBaseList[i].name + "(Clone)") 
                {
                    enemydata = enemyDataBaseList[i];
                    enemydata.index = monsterCount;
                    enemydata.enemyObject = enemyObject;
                    enemy.Setup(enemydata);
                    enemyInThisWaveList.Add(enemydata);
                }
            }
            monsterCount++;
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void NextWave()//현재 웨이브 정보로 에너미 프리팹 바꿔치기
    {
        //enemyPrefab 적오브젝트를 현재 웨이브정보 받아서  바꿔치기
        for(int i = 0; i < enemyDataBaseList.Count; i++)
        {
            if(enemyDataBaseList[i].wave == currentWave)
            {
                enemyPrefab = enemyDataBaseList[i].enemyObject;
                if(enemyDataBaseList[i].type == "boss")
                {
                    MaxCount = 1;
                }
                else
                {
                    MaxCount = TestMaxCount;
                }
            }
        }
        monsterCount = 0;
        CoroutineTrggier();
        currentWave++;
    }
}
