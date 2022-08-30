using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteSmash : MonoBehaviour
{
    List<Enemy> enemies;
    private void Start()
    {
        enemies = new List<Enemy>();
        Invoke("DelayDamage", 0.1f);
        Invoke("Destroythis", 1f);
    }
    private void DelayDamage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].thisEnemydata.hp = enemies[i].thisEnemydata.hp - (enemies[i].thisEnemydata.hp * 0.05f);
                enemies[i].Stunned(0);
                enemies[i].Hit();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Stunned(enemies[i].thisEnemydata.speed);
        }
        enemies.Clear();
    }
    void Destroythis()
    {
        Destroy(gameObject);
    }
}
