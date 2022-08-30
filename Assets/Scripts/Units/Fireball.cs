using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Damage;
    List<GameObject> enemies;
    private void Start()
    {
        enemies = new List<GameObject>();
        Invoke("DelayDamage", 0.1f);
    }
    private void DelayDamage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null) 
            { 
                enemies[i].GetComponent<Enemy>().thisEnemydata.hp = enemies[i].GetComponent<Enemy>().thisEnemydata.hp - (Damage);
                enemies[i].GetComponent<Enemy>().Hit();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
            enemies.Add(other.gameObject);
        }
    }
}
