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
    [SerializeField]
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
    public int MaxCount;
    public List<EnemyData> enemyList; //������� ����
    public List<EnemyData> enemyInThisWaveList; //������� ����
    // ���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ����
    private void Awake()
    {
        //������Ʈ �޸� �Ҵ�
        EnemyData enemyData = new EnemyData();
        enemyList = new List<EnemyData>();
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
            enemyList.Add(enemyData);
        }
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
            for (int i = 0; i < enemyList.Count; i++) //���ʹ� ����Ʈ���� �̸����� ���ʹ����� �����ͼ� �ش� ���ʹ� ������ ���ʹ� ���� �¾�
            {
                if (enemyObject.name == enemyList[i].name + "(Clone)") 
                {
                    enemydata = enemyList[i];
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

}
