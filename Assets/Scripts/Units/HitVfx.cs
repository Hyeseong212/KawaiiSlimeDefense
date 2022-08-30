using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfx : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 1f);
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
