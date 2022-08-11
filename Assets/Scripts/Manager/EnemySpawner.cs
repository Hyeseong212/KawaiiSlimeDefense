using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField]
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
    public int MaxCount;
    public List<Enemy> enemyList; //현재 맵에 존재하는 모든적의 정보
    // 적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요없다
    private void Awake()
    {
        //적리스트 메모리 할당
        enemyList = new List<Enemy>();
        //적생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (monsterCount != MaxCount)
        {
            GameObject clone = Instantiate(enemyPrefab, P1wayPoints[0].transform.position, Quaternion.identity); // 적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enmey 컴포넌트
            monsterCount++;
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
