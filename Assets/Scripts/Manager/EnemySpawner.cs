using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float difficulty;
    public int TestMaxCount;
    public List<EnemyData> enemyDataBaseList; //모든적의 정보
    public List<EnemyData> P1enemyInThisWaveList; //P1 현재 남은적의 정보
    public List<EnemyData> P2enemyInThisWaveList; //P2 현재 남은적의 정보
    public List<EnemyData> P3enemyInThisWaveList; //P3 현재 남은적의 정보
    public List<EnemyData> P4enemyInThisWaveList; //P4 현재 남은적의 정보
    //웨이브 정보
    public int currentWave = 0;

    // 적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요없다
    private void Awake()
    {
        if (GameManager.i.gameDifficulty == GameDifficulty.Easy)
        {
            difficulty = 1;
        }
        else if (GameManager.i.gameDifficulty == GameDifficulty.Normal)
        {
            difficulty = 1.5f;
        }
        else if (GameManager.i.gameDifficulty == GameDifficulty.Hard)
        {
            difficulty = 3f;
        }
        else if (GameManager.i.gameDifficulty == GameDifficulty.Hell)
        {
            difficulty = 6f;
        }
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
            enemyData.hp = Convert.ToSingle(data[i]["HP"])*difficulty;
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
            HPGenerator(enemyObject);
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
                    P1enemyInThisWaveList.Add(enemydata);
                }
            }
            GameManager.i.P1currentMonster.text = GameManager.i.PlayerID + " 남은 몬스터 수 : " + P1enemyInThisWaveList.Count.ToString();
            monsterCount++;
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void NextWave()//현재 웨이브 정보로 에너미 프리팹 바꿔치기
    {
        currentWave++;
        //enemyPrefab 적오브젝트를 현재 웨이브정보 받아서  바꿔치기
        for (int i = 0; i < enemyDataBaseList.Count; i++)
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
    }
    [SerializeField] GameObject m_goPrefab = null;
    [SerializeField] GameObject m_parents = null;
    public List<GameObject> m_hpBarList = new List<GameObject>();

    Camera m_cam;
    private void Start()
    {
        m_cam = Camera.main;

    }
    private void Update()
    {
        for (int i = 0; i < P1enemyInThisWaveList.Count; i++)
        {
            if (m_hpBarList[i] != null)
            {
                m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(P1enemyInThisWaveList[i].enemyObject.transform.position + new Vector3(0, 1.15f, 1.15f));
            }
        }
    }
    public void HPGenerator(GameObject _go)
    {
        //m_objectList.Add(_go.transform);
        GameObject t_hpbar = Instantiate(m_goPrefab, this.transform.position, Quaternion.identity, transform);
        t_hpbar.transform.SetParent(m_parents.transform);
        _go.GetComponent<Enemy>().HPbarSetup(t_hpbar);
        m_hpBarList.Add(t_hpbar);
    }
    public void HPbarDecrease(Enemy enemy)
    {
        if (enemy.gameObject != null)
        {
            enemy.HpBar.GetComponentsInChildren<Image>()[1].fillAmount = (enemy.thisEnemydata.hp / enemy.TotalHP);
        }
    }
    public void HPbarDestroy(GameObject _go)
    {
        m_hpBarList.Remove(_go);
        Destroy(_go);
    }
}
