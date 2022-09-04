using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloud : MonoBehaviour
{
    List<Enemy> enemies;
    public float Damage;
    float _tickTime = 0.5f;
    float _time = 0f;
    private void Start()
    {
        enemies = new List<Enemy>();
        Destroy(gameObject, 4.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().SpeedChange(other.gameObject.GetComponent<Enemy>().currentSpeed * 0.6f);
            enemies.Add(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().SpeedChange(other.gameObject.GetComponent<Enemy>().currentSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().SpeedChange(other.gameObject.GetComponent<Enemy>().thisEnemydata.speed);
            enemies.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SpeedChange(enemies[i].thisEnemydata.speed);
        }
        StopCoroutine("ThunderCloudDamage");
        enemies.Clear();
    }
    public void CoroutineTrigger()
    {
        StartCoroutine("ThunderCloudDamage");
    }
    IEnumerator ThunderCloudDamage()
    {
        while (true)
        {
            _time += Time.deltaTime;
            if (_time >= _tickTime)
            {
                _time = 0;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] != null)
                    {
                        enemies[i].GetComponent<Enemy>().thisEnemydata.hp = enemies[i].GetComponent<Enemy>().thisEnemydata.hp - (Damage * 8f);
                        enemies[i].GetComponent<Enemy>().Hit();
                    }
                }
            }
            yield return null;
        }
    }
}
