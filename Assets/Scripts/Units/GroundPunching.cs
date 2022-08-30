using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPunching : MonoBehaviour
{
    public float Damage;
    List<GameObject> enemies;
    private void Start()
    {
        enemies = new List<GameObject>();
        Invoke("DelayDamage", 0.1f);
        Invoke("DestroyThis", 1f);
    }
    private void DelayDamage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().thisEnemydata.hp = enemies[i].GetComponent<Enemy>().thisEnemydata.hp - (Damage *2);
            enemies[i].GetComponent<Enemy>().Hit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
