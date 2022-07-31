using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    Transform _tr;
    private void Start()
    {
        _tr = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    void FixedUpdate()
    {
        _tr.position = Input.mousePosition;
        if(Input.mousePosition.x >= -50 && Input.mousePosition.x < 50)
        {
            Camera.main.gameObject.transform.position += new Vector3(-1, 0, 0) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= (Screen.width - 50) && Input.mousePosition.x < (Screen.width + 50))
        {
            Camera.main.gameObject.transform.position += new Vector3(1, 0, 0) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= - 50 && Input.mousePosition.y <  50)
        {
            Camera.main.gameObject.transform.position += new Vector3(0,0,-1) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= (Screen.height - 50) && Input.mousePosition.y < (Screen.height + 50))
        {
            Camera.main.gameObject.transform.position += new Vector3(0, 0, 1) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
        }
    }
   
}
