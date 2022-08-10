using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] P1wayPoints;
    [SerializeField]
    private Transform[] P2wayPoints;
    [SerializeField]
    private Transform[] P3wayPoints;
    [SerializeField]
    private Transform[] P4wayPoints;

    public int monsterCount=0;
    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (monsterCount != 20)
        {
            GameObject clone = Instantiate(enemyPrefab); // �� ������Ʈ ����
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� Enmey ������Ʈ
            monsterCount++;
            enemy.Setup(P1wayPoints); //waypoint ������ �Ű������� Setup() ȣ��

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
