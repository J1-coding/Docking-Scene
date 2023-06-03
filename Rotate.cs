using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 point; // The point to rotate around
    public Vector3 axis;  // The axis to rotate around (ex. Vector3.up, Vector3.right, Vector3.forward, etc.)
    public float rotationSpeed; // The speed of the rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the point by the specified speed and axis
        transform.RotateAround(point, axis, rotationSpeed * Time.deltaTime);
    }
}
