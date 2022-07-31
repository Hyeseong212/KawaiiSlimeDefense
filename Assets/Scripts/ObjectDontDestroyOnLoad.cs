using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDontDestroyOnLoad : MonoSingleton<ObjectDontDestroyOnLoad>
{
    protected override void OnAwake()
    {
        if (ObjectDontDestroyOnLoad.IsInstance())
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
