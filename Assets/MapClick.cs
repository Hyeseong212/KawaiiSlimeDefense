using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapClick : MonoBehaviour
{
    Transform _thisTr;
    Vector3 mapPosition = new Vector3(1000,0,1000);
    void Start()
    {
        _thisTr = GetComponent<Transform>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
    void MapClickMethod()
    {
    }
}
