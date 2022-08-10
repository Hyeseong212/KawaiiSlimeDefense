using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public struct EnemyID
{
    public EnemyID(GameObject _enemy, int _enemyID)
    {
        this.enemy = _enemy;
        this.enemyID = _enemyID;
    }
    public GameObject enemy;
    public int enemyID;
}
public class EnemyCheck : MonoBehaviour
{
    [SerializeField] GameObject[] bullets;
    [SerializeField] Animator animator;
    List<EnemyID> enemies;
    EnemyID enemyIDclass;
    int enemyIndex;
    private void Start()
    {
        List<EnemyID> enemies = new List<EnemyID>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            //animator.SetInteger("MoveInt", 2);
            //enemyIDclass =new EnemyID(other.gameObject, enemyIndex);
            //enemies.Add(enemyIDclass);
            //enemyIndex++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (animator.GetInteger("MoveInt") != 1)
        {
            if (other.GetComponent<Collider>().CompareTag("Enemy"))
            {

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            //enemies.RemoveAt(enemies.Count);
        }
    }
    public void Shoot(Vector3 enemyPos)
    {

    }
}
