using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    List<Enemy> enemies;
    public float Damage;
    float _tickTime = 0.5f;
    float _time = 0f;
    private void Start()
    {
        enemies = new List<Enemy>();
        Destroy(gameObject, 4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnDestroy()
    {
        StopCoroutine("MeteorDamage");
        enemies.Clear();
    }
    public void CoroutineTrigger()
    {
        StartCoroutine("MeteorDamage");
    }
    IEnumerator MeteorDamage()
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
                        enemies[i].GetComponent<Enemy>().thisEnemydata.hp = enemies[i].GetComponent<Enemy>().thisEnemydata.hp - (Damage * 3f);
                        enemies[i].GetComponent<Enemy>().Hit();
                    }
                }
            }
            yield return null;
        }
    }
}
