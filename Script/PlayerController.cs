using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Move();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void Move()
    {
        Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        move = move.normalized * Mathf.Clamp(move.magnitude, 0.0f, 1.0f);
    }
}
