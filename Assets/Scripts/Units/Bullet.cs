using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject enemy;
    [SerializeField] float bulletSpeed;
    public void Setup(GameObject enemy)
    {
        this.enemy = enemy;
        StartCoroutine("ChaseEnemy");
    }
    IEnumerator ChaseEnemy()
    {
        while (true)
        {

            Vector3 newPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, bulletSpeed * Time.deltaTime);
            transform.LookAt(enemy.transform);
            yield return null;
        }
    }
}
