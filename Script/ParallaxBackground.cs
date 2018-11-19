using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public Camera   camera;
    public float    parallaxFactor;
    public bool     cursorActive = true;

    Vector3 relativeOffset;

	// Use this for initialization
	void Start ()
    {
        relativeOffset = transform.position - camera.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (cursorActive)
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos.x = mousePos.x / Screen.width;
            mousePos.y = mousePos.y / Screen.height;
            
            mousePos.x = mousePos.x * 2 - 1;
            mousePos.y = mousePos.y * 2 - 1;

            //            Vector3 delta = (camera.transform.position * parallaxFactor);
            Vector3 delta = (mousePos * parallaxFactor);            
            Vector3 pos = delta + relativeOffset;

            if ((mousePos.x < 0.0f) || (mousePos.y < 0.0f) || (mousePos.x > 1.0f) || (mousePos.y > 1.0f)) pos = Vector3.zero;

            pos = transform.position + (pos - transform.position) * 0.1f;
            pos.z = transform.position.z;
            transform.position = pos;
        }
	}
}
