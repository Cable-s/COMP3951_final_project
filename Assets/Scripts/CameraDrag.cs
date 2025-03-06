using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CameraDrag : MonoBehaviour
{
    private float dragSpeed = 0.1f;
    private Vector3 dragOrigin;

    //update method is builtin, runs every frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - dragOrigin;
            transform.Translate(delta.x * dragSpeed * -1, delta.y * dragSpeed * -1, 0);
            dragOrigin = Input.mousePosition;
        }
        float fov = Camera.main.orthographicSize;
        fov -= Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = fov;
    }
}
