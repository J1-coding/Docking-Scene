using System.Collections;
using UnityEngine;
using Cinemachine;

public class UAVController2 : MonoBehaviour
{
    private float downSpeed = 5f;
    public float descendDistance = 1f; // The amount of distance to descend
    public float descendDuration = 2f; // The duration of descending

    public float straightSpeed = 200f; // The speed at which the aircraft moves
    public GameObject target; // the Basket
    public GameObject target2; // the Basket2
    public float guideSpeed = 10f; // speed of the UAV
    public float rotationSpeed = 0.05f; // The factor for smooth rotation
    private float attachmentSpeed = 0.5f; // speed of smooth attachment to the target
    private float attachmentSpeed2 = 0.05f; // speed of smooth attachment to the target
    private bool guideRoute = false;
    private bool isAttaching = false; // indicate if the UAV is attaching to the target
    private Rigidbody rb;
    private Vector3 endPosition; // position to move towards during the attachment
    private Quaternion endRotation; // rotation to move towards during the attachment

    private bool localMove = false;
    private bool isDescending = false; // indicate if the UAV is descending

    private bool wingOpen = false;
    private bool wingClose = false;
    public GameObject RwingRotatePivot;
    public GameObject LwingRotatePivot;

    public CinemachineDollyCart dollyCart;
    private float newSpeed = 15f;
    private float newSpeed2 = 10f;
    private float newSpeed3 = 8.5f;
    private float newSpeed4 = 11.5f;
    private float newSpeed5 = 10f;
    public CinemachineVirtualCamera vcam;
    public Transform newVcamTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        //At first, the wing maintain to be folded.
        if (wingOpen)
        {
            StartCoroutine(WingOpen());
        }

        if (wingClose)
        {
            StartCoroutine(WingClose());
        }

        if (localMove)
        {
            /*ActivateLocalMove();*/
            endPosition = target2.transform.position;
            endRotation = target2.transform.rotation;

            transform.position = Vector3.Lerp(transform.position, endPosition, attachmentSpeed2);
            transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, attachmentSpeed2);

            transform.SetParent(target2.transform);
            print("========================================END========================================");
            /* GetComponent<UAVController>().enabled = false;*/
        }
        else
        {
            //UAV Get out of aircraft
            if (isDescending)
            {
                transform.position -= Vector3.up * downSpeed * Time.deltaTime;
            }
            //Going to the target1
            if (isAttaching)
            {
                // smoothly move position and rotation towards the target1
                transform.position = Vector3.Lerp(transform.position, endPosition, attachmentSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, attachmentSpeed);

                // stop attaching when the UAV is sufficiently close to the target1
                if (Vector3.Distance(transform.position, endPosition) < 0.01f)
                {
                    transform.SetParent(target.transform);
                    isAttaching = false;
                    wingClose = true;
                    localMove = true;
                }
            }
            else
            {
                //local Move to smoothly and straightly get into the basket, perfectly fit to the basket
                rb.velocity = transform.forward * (guideRoute ? guideSpeed : straightSpeed);

                if (guideRoute)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }

        }

    }

    IEnumerator WingOpen()
    {
        if (dollyCart != null)
        {
            dollyCart.m_Speed = newSpeed2;
        }
        Quaternion RinitialRotation = RwingRotatePivot.transform.rotation;
        Quaternion LinitialRotation = LwingRotatePivot.transform.rotation;

        Quaternion RtargetRotation = Quaternion.Euler(RinitialRotation.eulerAngles + new Vector3(0, 0, -25f));
        Quaternion LtargetRotation = Quaternion.Euler(LinitialRotation.eulerAngles + new Vector3(0, 0, 25f));

        float startTime = Time.time;
        float overTime = 0.05f; //duration of rotation
        while (Time.time - startTime < overTime)
        {
            RwingRotatePivot.transform.rotation = Quaternion.Lerp(RinitialRotation, RtargetRotation, (Time.time - startTime) / overTime);
            LwingRotatePivot.transform.rotation = Quaternion.Lerp(LinitialRotation, LtargetRotation, (Time.time - startTime) / overTime);
            yield return null;
        }

        // Ensure the rotation completes
        RwingRotatePivot.transform.rotation = RtargetRotation;
        LwingRotatePivot.transform.rotation = LtargetRotation;

        wingOpen = false;
    }

    IEnumerator WingClose()
    {
        Quaternion RinitialRotation = RwingRotatePivot.transform.rotation;
        Quaternion LinitialRotation = LwingRotatePivot.transform.rotation;

        Quaternion RtargetRotation = Quaternion.Euler(RinitialRotation.eulerAngles + new Vector3(0, 0, 25f));
        Quaternion LtargetRotation = Quaternion.Euler(LinitialRotation.eulerAngles + new Vector3(0, 0, -25f));

        float startTime = Time.time;
        float overTime = 0.05f; //duration of rotation
        while (Time.time - startTime < overTime)
        {
            RwingRotatePivot.transform.rotation = Quaternion.Lerp(RinitialRotation, RtargetRotation, (Time.time - startTime) / overTime);
            LwingRotatePivot.transform.rotation = Quaternion.Lerp(LinitialRotation, LtargetRotation, (Time.time - startTime) / overTime);
            yield return null;
        }

        // Ensure the rotation completes
        RwingRotatePivot.transform.rotation = RtargetRotation;
        LwingRotatePivot.transform.rotation = LtargetRotation;

        wingClose = false;
    }

    // Method to be called by JetController script
    public void ActivateGuidedRoute()
    {
        if (dollyCart != null)
        {
            dollyCart.m_Speed = newSpeed;
        }
        StartCoroutine(StartDescendingAfterDelay(0.5f)); // start descending after certain seconds
        StartCoroutine(StartGuidedRoute());
    }

    IEnumerator StartGuidedRoute()
    {
        // Wait for a bit before starting the rotation
        yield return new WaitForSeconds(1.5f);
        //Now the UAV wing has to be spreaded
        wingOpen = true;
        guideRoute = true;
    }

    IEnumerator StartDescendingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDescending = true;

        float timer = 0f;
        while (timer < descendDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isDescending = false;
    }

    // Method to stop moving when it reaches the target
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            guideRoute = false;
            rb.velocity = Vector3.zero; // stop the UAV from moving forward
            rb.isKinematic = true; // deactivate the physics on the UAV
            endPosition = target.transform.position;
            endRotation = target.transform.rotation;
            isAttaching = true; // start the process of attachment
        }

        // Check if the aircraft has collided with the transparent plane
        if (other.gameObject.CompareTag("TransparentPlane2"))
        {
            //Dolby cart look at truck with reducing speed
            dollyCart.m_Speed = newSpeed3;
        }

        if (other.gameObject.CompareTag("TransparentPlane3"))
        {
            dollyCart.m_Speed = newSpeed4;
        }

        if (other.gameObject.CompareTag("TransparentPlane4"))
        {
            vcam.LookAt = newVcamTarget;
            dollyCart.m_Speed = newSpeed5;
        }
    }

}

