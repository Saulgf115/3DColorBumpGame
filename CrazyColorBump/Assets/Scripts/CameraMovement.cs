using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 6.0f;

    public Vector3 cameraVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * cameraSpeed * Time.deltaTime;

        cameraVelocity = Vector3.forward * cameraSpeed * Time.deltaTime; 
    }
}
