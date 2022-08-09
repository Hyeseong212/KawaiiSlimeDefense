using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            Debug.Log("적이다!!!");
        }
    }
}
