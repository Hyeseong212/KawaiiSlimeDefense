using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
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
    public List<Enemy> enemyList; //���� �ʿ� �����ϴ� ������� ����
    // ���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ����
    private void Awake()
    {
        //������Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        //������ �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (monsterCount != MaxCount)
        {
            GameObject clone = Instantiate(enemyPrefab, P1wayPoints[0].transform.position, Quaternion.identity); // �� ������Ʈ ����
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� Enmey ������Ʈ
            monsterCount++;
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
