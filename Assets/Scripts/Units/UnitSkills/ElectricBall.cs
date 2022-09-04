using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    List<Enemy> enemies;
    [SerializeField] GameObject HitVfx;
    public Vector3 Pos;
    public float Damage;
    float _tickTime = 0.25f;
    float _time = 0f;
    private void Start()
    {
        enemies = new List<Enemy>();
        Destroy(gameObject, 6f);
    }
    private void Update()
    {
        if (Pos != null)
        {
            transform.Translate(Pos * 0.2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(HitVfx, other.gameObject.transform.position+ new Vector3(0, 1, 0), Quaternion.identity);
            other.gameObject.GetComponent<Enemy>().thisEnemydata.hp = other.gameObject.GetComponent<Enemy>().thisEnemydata.hp - (Damage * 1.5f);
            other.gameObject.GetComponent<Enemy>().Hit();
            enemies.Add(other.gameObject.GetComponent<Enemy>());
        }
        else if(other.tag == "Wall")
        {
            Destroy(gameObject);
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
        Instantiate(HitVfx, this.transform.position, Quaternion.identity);
        StopCoroutine("ElectricBallDamage");
        enemies.Clear();
    }
    public void CoroutineTrigger()
    {
        StartCoroutine("ElectricBallDamage");
    }
    IEnumerator ElectricBallDamage()
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
                        Instantiate(HitVfx, enemies[i].gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                        enemies[i].GetComponent<Enemy>().thisEnemydata.hp = enemies[i].GetComponent<Enemy>().thisEnemydata.hp - (Damage * 1.5f);
                        enemies[i].GetComponent<Enemy>().Hit();
                    }
                }
            }
            yield return null;
        }
    }
}
