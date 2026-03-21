// FlightController.cs
// CENG 454 - HW1: Sky-High Prototype
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed  = 45f;  // degrees/second
    [SerializeField] private float yawSpeed    = 45f;  // degrees/second
    [SerializeField] private float rollSpeed   = 45f;  // degrees/second
    [SerializeField] private float thrustSpeed = 5f;   // units/second

    // Task 3-A: Rigidbody reference for physics integration
    private Rigidbody rb;

    void Start()
    {
        // Task 3-B: Cache Rigidbody and disable physics-driven rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    private void HandleRotation()
    {
        // Task 3-C: Pitch Arrow Up/Down rotates around the X-axis (Vector3.right)
        float pitch = Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, pitch);

        // Task 3-C: Yaw Arrow Left/Right rotates around the Y-axis (Vector3.up)
        float yaw = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yaw);

        // Task 3-C: Roll Q/E keys rotate around the Z-axis (Vector3.forward)
        float roll = 0f;
        if (Input.GetKey(KeyCode.Q))
            roll = rollSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E))
            roll = -rollSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, roll);
    }

    private void HandleThrust()
    {
        // Task 3-D: Thrust Spacebar moves the aircraft forward along its local Z-axis
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime);
        }
    }
}

