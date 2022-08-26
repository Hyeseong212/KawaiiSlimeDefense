using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;
    private int maxChainCount;
    SlimeData slimedata;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject HitVfx;
    private List<GameObject> enemyList;
    private float cushion;

    public void Setup(GameObject enemy, SlimeData slimedata)
    {
        enemyList = new List<GameObject>();
        enemyList.Add(enemy);
        this.enemy = enemy;
        this.slimedata = slimedata;
        maxChainCount = 0;
        cushion = 1;
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
                    if (gameObject.name == "ChainLightening(Clone)")
                    {
                        enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts * cushion);
                        enemy.GetComponent<Enemy>().Hit();
                        if (maxChainCount <= 2)
                        {
                            NextMoveTo();
                        }
                        else if(maxChainCount == 3)
                        {
                            Destroy(this.gameObject);
                        }

                    }//레어 노랑슬라임
                    else if(gameObject.name == "Fireball(Clone)")
                    {
                        GameObject BoomVfx = Instantiate(HitVfx, transform.position,Quaternion.Euler(270,0,0));
                        BoomVfx.GetComponent<Fireball>().Damage = slimedata.attackpts;
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                        enemy.GetComponent<Enemy>().Hit();
                        Instantiate(HitVfx, transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        maxChainCount++;
        cushion = cushion * 0.5f;
        if (enemyList.Count <= 3)
        {
            enemy = enemyList[enemyList.Count-1];
        }
        else 
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (!other.gameObject.GetComponent<Enemy>().isDead)
            {
                if (other.gameObject != enemy)
                {
                    if (enemyList.Count <= 3)
                    {
                        enemyList.Add(other.gameObject);
                    }
                }
            }
        }
    }
}
