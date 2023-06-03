using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float downhillSpeed = 10f;
    public float straightSpeed = 5f;
    public float stopTime = 5f;
    public float downhillTime = 5f;
    public float straightTime = 5f;

    private Rigidbody rb;
    private float timer;
    private enum State { Downhill, Straight, Stopping }
    private State currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = State.Downhill;
        timer = Time.time + downhillTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Downhill:
                GoDownhill();
                if (Time.time >= timer)
                {
                    currentState = State.Straight;
                    timer = Time.time + straightTime;
                }
                break;
            case State.Straight:
                GoStraight();
                if (Time.time >= timer)
                {
                    currentState = State.Stopping;
                    timer = Time.time + stopTime;
                }
                break;
            case State.Stopping:
                StopSmoothly();
                if (Time.time >= timer)
                {
                    currentState = State.Downhill;
                    timer = Time.time + downhillTime;
                }
                break;
        }
    }

    void GoDownhill()
    {
        Vector3 movement = new Vector3(0, -downhillSpeed * Time.deltaTime, 0);
        rb.MovePosition(rb.position + movement);
    }

    void GoStraight()
    {
        Vector3 movement = new Vector3(0, 0, straightSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + movement);
    }

    void StopSmoothly()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity.normalized * straightSpeed / stopTime * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
