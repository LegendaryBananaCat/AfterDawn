using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    public Camera camera;

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        
        mousePos.x = mousePos.x / Screen.width;
        mousePos.y = mousePos.y / Screen.height;

        mousePos.x = mousePos.x * 2 - 1;
        mousePos.y = mousePos.y * 2 - 1;

        mousePos.y = mousePos.y * camera.orthographicSize;
        mousePos.x = mousePos.x * camera.orthographicSize * camera.aspect;

        Vector3 cameraPos = camera.transform.position;
        cameraPos.z = 0.0f;

        transform.position = mousePos + cameraPos;
    }
}
