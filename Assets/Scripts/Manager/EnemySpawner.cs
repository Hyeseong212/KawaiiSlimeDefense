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
    private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private float spawnTime; //�� ���� �ֱ�
    [SerializeField]
    public Transform[] P1wayPoints; //Player 1�� �̵����
    [SerializeField]
    public Transform[] P2wayPoints; //Player 2�� �̵����
    [SerializeField]
    public Transform[] P3wayPoints; //Player 3�� �̵����
    [SerializeField]
    public Transform[] P4wayPoints; //Player 4�� �̵����

    int monsterCount=0;
    int MaxCount;
    public int TestMaxCount;
    public List<EnemyData> enemyDataBaseList; //������� ����
    public List<EnemyData> enemyInThisWaveList; //������� ����
    //���̺� ����
    public int currentWave = 0;

    // ���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ����
    private void Awake()
    {
        //������Ʈ �޸� �Ҵ�
        EnemyData enemyData = new EnemyData();
        enemyDataBaseList = new List<EnemyData>();
        //������ �ڷ�ƾ �Լ� ȣ��
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
            GameObject enemyObject = Instantiate(enemyPrefab, P1wayPoints[0].transform.position, Quaternion.identity); // �� ������Ʈ ����
            Enemy enemy = enemyObject.GetComponent<Enemy>(); // ��� ������ ���� Enmey ������Ʈ
            enemyObject.transform.parent = enemiesParent.transform;
            EnemyData enemydata = new EnemyData();
            for (int i = 0; i < enemyDataBaseList.Count; i++) //���ʹ� ����Ʈ���� �̸����� ���ʹ����� �����ͼ� �ش� ���ʹ� ������ ���ʹ� ���� �¾�
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
    public void NextWave()//���� ���̺� ������ ���ʹ� ������ �ٲ�ġ��
    {
        //enemyPrefab ��������Ʈ�� ���� ���̺����� �޾Ƽ�  �ٲ�ġ��
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
