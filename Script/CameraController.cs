using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode {  Target, Velocity, Cursor };

    public CameraMode           mode;
    public Transform            target;
    public ParallaxBackground   bG;
    public float                cameraFollowSpeed = 0.05f;
    public float                lookAhead = 25.0f;
    public Vector3              cameraLimit = new Vector3(16.0f, 9.0f);


    Camera camera;
    Vector3 cursor;
    public bool active = true;
    float maxDisX;
    float maxDisY;

    // Use this for initialization
    void Awake ()
    {
        camera = GetComponent<Camera>();
	}

    private void Start()
    {
        bool outOfBounds;
        cursor = GetMouseCursor(out outOfBounds);

        if (outOfBounds) target.position = Vector3.zero;
        else target.position = transform.position;
    }

    Vector3 GetMouseCursor(out bool outOfBounds)
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.x = mousePos.x / Screen.width;
        mousePos.y = mousePos.y / Screen.height;

        if ((mousePos.x < 0.0f) || (mousePos.y < 0.0f) || (mousePos.x > 1.0f) || (mousePos.y > 1.0f)) outOfBounds = true;
        else outOfBounds = false;

        mousePos.x = mousePos.x * 2 - 1;
        mousePos.y = mousePos.y * 2 - 1;

        mousePos.y = mousePos.y * camera.orthographicSize;
        mousePos.x = mousePos.x * camera.orthographicSize * camera.aspect;

        Vector3 cameraPos = camera.transform.position;
        cameraPos.z = 0.0f;

        return mousePos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool outOfBounds;
        cursor = GetMouseCursor(out outOfBounds);

        maxDisX = Mathf.Abs(cursor.x - target.transform.position.x);
        maxDisY = Mathf.Abs(cursor.y - target.transform.position.y);

        if (target == null) return;

        Vector3 targetPosition = Vector3.zero;

        /*
        if (maxDisX > (camera.orthographicSize * 16) / 9 || maxDisY > camera.orthographicSize)
        {
            bG.cursorActive = false;
            active = false;
        }

        if (maxDisX < ((camera.orthographicSize * 16) / 9) - 0.75f && maxDisY < camera.orthographicSize - 0.75f)
        {
            bG.cursorActive = true;
            active = true;

        }*/

        switch (mode)
        {
            case CameraMode.Target:
                targetPosition = target.position;
                break;
            case CameraMode.Velocity:
                {
                    Rigidbody2D targetRB = target.GetComponent<Rigidbody2D>();
                    if (targetRB == null)
                    {
                        targetPosition = target.position + target.transform.up * lookAhead;
                    }
                    else
                    {
                        Vector3 velocity = targetRB.velocity;
                        targetPosition = target.position + velocity * lookAhead;
                    }
                }
                break;
            case CameraMode.Cursor:
                {
                    if (active)
                    {
                        if (outOfBounds) targetPosition = Vector3.zero;
                        else targetPosition = cursor; // Vector3.Lerp(transform.position, cursor, 0.1f);
                    }
                }
                break;
        }
        
        Vector3 currentPos = camera.transform.position;
        Vector3 error = targetPosition - currentPos; error.z = 0.0f;

        Vector3 newPos = currentPos + error * cameraFollowSpeed;
        newPos.x = Mathf.Clamp(newPos.x, -cameraLimit.x, cameraLimit.x);
        newPos.y = Mathf.Clamp(newPos.y, -cameraLimit.y, cameraLimit.y);
        camera.transform.position = newPos;
    }
}
