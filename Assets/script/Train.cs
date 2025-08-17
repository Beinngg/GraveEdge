using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    [Header("Destination")]
    public float stopZ = -512.7f;

    [HideInInspector] public bool startMoving = false;

    private float currentSpeed = 0f;
    private bool moving = false;

    void Update()
    {
        if (!startMoving) return;

        moving = true;
        float distanceRemaining = transform.position.z - stopZ;
        float stoppingDistance = (currentSpeed * currentSpeed) / (2f * deceleration);

        if (distanceRemaining > stoppingDistance)
        {
            // Accelerate
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Decelerate
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        float moveStep = currentSpeed * Time.deltaTime;

        if (transform.position.z - moveStep <= stopZ)
        {
            // Snap to stop point
            transform.position = new Vector3(transform.position.x, transform.position.y, stopZ);
            currentSpeed = 0f;
            moving = false;
            startMoving = false;
            Debug.Log("🚆 Train stopped at destination.");
        }
        else
        {
            transform.Translate(Vector3.back * moveStep);
        }
    }
}
