using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject enemy;
    SlimeData slimedata;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject HitVfx;
    public void Setup(GameObject enemy, SlimeData slimedata)
    {
        this.enemy = enemy;
        this.slimedata = slimedata;
        StartCoroutine("ChaseEnemy");
    }
    IEnumerator ChaseEnemy()
    {
        while (true)
        {
            if (enemy != null)
            {
                Vector3 newPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, bulletSpeed * Time.deltaTime);
                transform.LookAt(enemy.transform);
                if (Vector3.Distance(transform.position, enemy.transform.position) <= 0f)
                {
                    enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - slimedata.attackpts;
                    Instantiate(HitVfx, transform.position, Quaternion.identity);
                    enemy.GetComponent<Enemy>().Hit();
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Instantiate(HitVfx, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
}
