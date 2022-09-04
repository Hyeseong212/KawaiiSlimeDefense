using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunch : MonoBehaviour
{
    List<Enemy> enemies;
    [SerializeField] GameObject HitVfx;
    private void Start()
    {
        enemies = new List<Enemy>();
        Invoke("DelayDamage", 0.5f);
        Invoke("Destroythis", 1.5f);
    }
    private void DelayDamage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                Instantiate(HitVfx, enemies[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                enemies[i].thisEnemydata.hp = enemies[i].thisEnemydata.hp - (enemies[i].thisEnemydata.hp * 0.2f);
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
            enemies[i].SpeedChange(enemies[i].thisEnemydata.speed);
        }
        enemies.Clear();
    }
    void Destroythis()
    {
        Destroy(gameObject);
    }
}
