using UnityEngine;
using System.Collections;


public class enermy2 : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private float turretRange = 10f;
    [SerializeField] private float turretRotationSpeed = 5f;

    [Header("Recoil / Knockback")]
    public float moveBackDistance = 10f;
    public float moveDuration = 0.5f;

    [Tooltip("Collider radius used to test for obstacles while moving back.")]
    [SerializeField] private float stopCollisionRadius = 0.4f;

    [Tooltip("Layers considered solid for stopping the back move.")]
    [SerializeField] private LayerMask stopLayers = ~0; // everything by default

    [Tooltip("Ignore trigger colliders when checking for obstacles.")]
    [SerializeField] private bool ignoreTriggers = true;

    private Transform PlayerTransform;
    private shooting CurrentShooting;
    private float fireRate;
    private float fireRateDelta;

    private Collider ownCollider;

    private void Start()
    {
        PlayerTransform = FindObjectOfType<FirstPersonController>()?.transform;
        if (PlayerTransform == null)
        {
            Debug.LogError("FirstPersonController not found in the scene!");
            return;
        }

        CurrentShooting = GetComponentInChildren<shooting>();
        if (CurrentShooting == null)
        {
            Debug.LogError("No 'shooting' component found in " + gameObject.name + " or its children!");
            return;
        }

        fireRate = CurrentShooting.RateOffFire();
        ownCollider = GetComponent<Collider>(); 
    }

    private void Update()
    {
        if (PlayerTransform == null) return;

        Vector3 playerGroundPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        if (Vector3.Distance(transform.position, playerGroundPos) > turretRange) return;

        Vector3 playerDirection = playerGroundPos - transform.position;
        float step = turretRotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, playerDirection, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // Fire
        fireRateDelta -= Time.deltaTime;
        if (fireRateDelta <= 0f)
        {
            CurrentShooting.Fire();
            fireRateDelta = fireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

 
    public void MoveBackSmoothly()
    {
        StopAllCoroutines();
        StartCoroutine(MoveBackCoroutine());
    }

 
    private IEnumerator MoveBackCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - transform.forward * moveBackDistance;
        float elapsed = 0f;


        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            Vector3 targetThisFrame = Vector3.Lerp(startPos, endPos, t);


            Vector3 from = transform.position;
            Vector3 to = targetThisFrame;
            Vector3 delta = to - from;

            if (delta.sqrMagnitude > 0.000001f)
            {
                float dist = delta.magnitude;
                Vector3 dir = delta / dist;


                QueryTriggerInteraction qti = ignoreTriggers ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide;
                if (Physics.SphereCast(from, stopCollisionRadius, dir, out RaycastHit hit, dist, stopLayers, qti))
                {
            
                    if (ownCollider != null && hit.collider.transform.IsChildOf(transform))
                    {
                
                    }
                    else
                    {
                        // Place just before the hit point and stop the coroutine
                        transform.position = hit.point - dir * stopCollisionRadius;
                        yield break;
                    }
                }

      
                transform.position = to;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
