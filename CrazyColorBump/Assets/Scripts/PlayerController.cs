using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Atrributes")]
    Rigidbody rb;
    Vector3 lastMousePosition;
    public float sensivity = 0.16f;
    public float clampDelta = 42.0f;

    public float bounds = 5;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-bounds,bounds),transform.position.y,transform.position.z);
        transform.position += FindObjectOfType<CameraMovement>().cameraVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 vector = lastMousePosition - Input.mousePosition;

            lastMousePosition = Input.mousePosition;

            vector = new Vector3(vector.x, 0.0f, vector.y);

            Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
            rb.AddForce(/*Vector3.forward * 2 +*/ (-moveForce * sensivity - rb.velocity / 5.0f),ForceMode.VelocityChange);
            //rb.AddForce(-moveForce * sensivity - rb.velocity / 5, ForceMode.VelocityChange);
        }

        rb.velocity.Normalize();
    }
}
