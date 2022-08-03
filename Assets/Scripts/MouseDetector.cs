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
        Debug.Log(Camera.main.gameObject.transform.position);
    }
    void FixedUpdate()
    {
        _tr.position = Input.mousePosition;
        if (Camera.main.gameObject.transform.position.x < 115 && Camera.main.gameObject.transform.position.x > -115
            && Camera.main.gameObject.transform.position.z < 130 && Camera.main.gameObject.transform.position.z > -130)
        {
            if (Input.mousePosition.x >= -50 && Input.mousePosition.x < 50)
            {
                Camera.main.gameObject.transform.position += new Vector3(-1, 0, 0) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= (Screen.width - 50) && Input.mousePosition.x < (Screen.width + 50))
            {
                Camera.main.gameObject.transform.position += new Vector3(1, 0, 0) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y >= -50 && Input.mousePosition.y < 50)
            {
                Camera.main.gameObject.transform.position += new Vector3(0, 0, -1) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y >= (Screen.height - 50) && Input.mousePosition.y < (Screen.height + 50))
            {
                Camera.main.gameObject.transform.position += new Vector3(0, 0, 1) * GlobalOptions.i.options.mapMoveSpeed * Time.deltaTime;
            }
        }
        else if(Camera.main.gameObject.transform.position.x >= 115)
        {
            Camera.main.gameObject.transform.position = new Vector3(114, GlobalOptions.i.options.cameraYvalue, Camera.main.gameObject.transform.position.z);
        }
        else if(Camera.main.gameObject.transform.position.x <= -115)
        {
            Camera.main.gameObject.transform.position = new Vector3(-114, GlobalOptions.i.options.cameraYvalue, Camera.main.gameObject.transform.position.z);
        }
        else if (Camera.main.gameObject.transform.position.z >= 130)
        {
            Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, GlobalOptions.i.options.cameraYvalue, 129);
        }
        else if (Camera.main.gameObject.transform.position.z <= -130)
        {
            Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, GlobalOptions.i.options.cameraYvalue, -129);
        }
    }

   
}
