using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce = 20f;
    public float maxWallRunTime = 1.5f;
    public float wallCheckDistance = 0.6f;
    public float minJumpHeight = 1.5f;
    public float wallClimbSpeed = 3f;
    public float exitWallTime = 0.2f;

    [Header("References")]
    public Transform orientation;

    private Rigidbody rb;
    private bool wallLeft;
    private bool wallRight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool isWallRunning;
    private float wallRunTimer;
    private bool exitingWall;
    private float exitWallTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForWalls();

        if ((wallLeft || wallRight) && AboveGround())
        {
            if (!isWallRunning && !exitingWall)
                StartWallRun();

            if (isWallRunning)
                wallRunTimer += Time.deltaTime;

            if (wallRunTimer > maxWallRunTime)
                StopWallRun();
        }
        else
        {
            if (isWallRunning)
                StopWallRun();
        }

        if (exitingWall)
        {
            exitWallTimer -= Time.deltaTime;
            if (exitWallTimer <= 0)
                exitingWall = false;
        }

        // Wall jump
        if (isWallRunning && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 wallNormal = wallLeft ? leftWallHit.normal : rightWallHit.normal;
            Vector3 jumpDir = wallNormal + Vector3.up;

            // Reset Y velocity before jumping
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(jumpDir.normalized * wallRunForce, ForceMode.Impulse);

            StopWallRun();
            exitingWall = true;
            exitWallTimer = exitWallTime;
        }
    }

    private void FixedUpdate()
    {
        if (isWallRunning)
        {
            Vector3 wallDirection = Vector3.zero;

            if (wallLeft)
                wallDirection = Vector3.Cross(leftWallHit.normal, Vector3.up);
            else if (wallRight)
                wallDirection = Vector3.Cross(Vector3.up, rightWallHit.normal);

            wallDirection.Normalize();

            // Apply force along wall direction
            rb.AddForce(wallDirection * wallRunForce, ForceMode.Force);

            Vector3 currentVelocity = rb.linearVelocity;

            // Vertical control


            rb.linearVelocity = currentVelocity;
        }
    }

    private void CheckForWalls()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StartWallRun()
    {
        isWallRunning = true;
        wallRunTimer = 0;
        rb.useGravity = false;
    }

    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }
}
