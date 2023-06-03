using System.Collections;
using UnityEngine;

public class JetController : MonoBehaviour
{
    public float speed = 20f; // The speed at which the aircraft moves
    public float rotationSpeed = 0.05f; // The factor for smooth rotation

    public float rollAngle = 80f; // The angle for the roll rotation
    public float yawAngle = -160f; // The angle for the yaw rotation
    public float pitchAngle = -20f; // The angle for the pitch rotation

    private Rigidbody rb;
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        print("int he ");
        // Always apply forward velocity
        rb.velocity = transform.forward * speed;

        // If rotating, smoothly rotate over time
        if (isRotating)
        {
            print("in fixed update");
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the aircraft has collided with the transparent plane
        if (other.gameObject.CompareTag("TransparentPlane"))
        {
            // Start rotating the aircraft
            isRotating = true;

            // Convert euler angles to quaternion and apply it as the target rotation
            // Note the order: pitch (x), yaw (y), roll (z)
            targetRotation = Quaternion.Euler(pitchAngle, yawAngle, rollAngle);

            GameObject uav = GameObject.FindWithTag("UAV"); // Get the UAV object
            uav.SendMessage("ActivateGuidedRoute"); // Send a message to activate the guided route
            speed = 33f;

            print("in trigger");
        }
    }
}


/*using System.Collections;
using UnityEngine;

public class JetController : MonoBehaviour
{
    public float speed = 20f; // The speed at which the aircraft moves
    public float rotationSpeed = 0.005f; // The factor for smooth rotation
    public float moveTime = 5f; // The time for which the aircraft moves straight

    public float rollAngle = 80f; // The angle for the roll rotation
    public float yawAngle = -160f; // The angle for the yaw rotation
    public float pitchAngle = -20f; // The angle for the pitch rotation

    private Rigidbody rb;
    private Quaternion targetRotation;
    private bool isRotating;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveStraight());
    }

    void FixedUpdate()
    {
        if (!isRotating)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            // Smoothly rotate over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
            rb.velocity = transform.forward * speed; // Move in the direction the aircraft is facing
        }
    }

    IEnumerator MoveStraight()
    {
        yield return new WaitForSeconds(moveTime);

        // After moving straight for a certain time, rotate the aircraft
        isRotating = true;

        // Convert euler angles to quaternion and apply it as the target rotation
        // Note the order: pitch (x), yaw (y), roll (z)
        targetRotation = Quaternion.Euler(pitchAngle, yawAngle, rollAngle);
    }
}*/
