using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {

    public float mouseSensitivity = 1.0f;
    private Vector3 lastPosition;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //pan movements
        if (Input.GetMouseButtonDown(2))
        {
            lastPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }
}
