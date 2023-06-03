using UnityEngine;

public class TruckController : MonoBehaviour
{
    public float speed = 5f; // Speed at which the truck moves forward
    private Rigidbody rb;

    private float total;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    private void Update()
    {
        total += Time.deltaTime;
        if(total>60f)
        {
            rb.velocity = transform.forward * 0;
        }
    }
}
