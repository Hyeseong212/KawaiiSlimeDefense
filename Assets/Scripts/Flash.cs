using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 0.5f);
    }
    void SetUp(GameObject enemy)
    {
        Quaternion.LookRotation(enemy.transform.position);
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
