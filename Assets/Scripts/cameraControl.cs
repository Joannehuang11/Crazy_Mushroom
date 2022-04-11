using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public Camera myCamera;
    public float scrollSensitivity = 20;
    public float penSensitivity = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myCamera.orthographicSize += (Input.GetAxis("Mouse ScrollWheel")) * scrollSensitivity;

        if (Input.GetKey("w"))
        {
            transform.position += (transform.up * penSensitivity);
        }

        if (Input.GetKey("a"))
        {
            transform.position -= (transform.right * penSensitivity);
        }

        if (Input.GetKey("d"))
        {
            transform.position += (transform.right * penSensitivity);
        }

        if (Input.GetKey("s"))
        {
            transform.position -= (transform.up * penSensitivity);
        }
    }
}
