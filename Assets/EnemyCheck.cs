using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] GameObject[] bullets;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            Debug.Log("적이다!!!");
            Shoot(other.GetComponent<Transform>().position);
        }
    }
    public void Shoot(Vector3 enemyPos)
    {

    }
}
