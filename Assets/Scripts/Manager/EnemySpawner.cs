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
            GameObject clone = Instantiate(enemyPrefab); // 적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enmey 컴포넌트
            monsterCount++;
            enemy.Setup(P1wayPoints); //waypoint 정보를 매개변수로 Setup() 호출

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
