using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrollingAI : MonoBehaviour
{
    [Header("Waypoints (must be on baked NavMesh)")]
    public Transform[] waypoints;

    [Header("Pathing")]
    [SerializeField] private bool pingPong = false;          // bounce 0..N..0
    [SerializeField] private float arriveThreshold = 0.5f;   // extra buffer beyond stoppingDistance
    [SerializeField] private bool waitAtWaypoint = false;
    [SerializeField] private float waitSeconds = 0.25f;

    [Header("Startup")]
    [SerializeField] private bool warpToNavMeshAtStart = true;
    [SerializeField] private float warpSearchRadius = 2f;    // search radius to snap to mesh

    private NavMeshAgent agent;
    private int index = 0;
    private int dir = 1;                                     // for ping‑pong
    private bool waiting = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // 3D setup (XZ plane)
        agent.updateRotation = true;  // let agent face its velocity
        agent.updateUpAxis   = true;  // critical for 3D
    }

    private void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("[PatrollingAI] No waypoints assigned.", this);
            enabled = false;
            return;
        }

        // Ensure the agent starts on a NavMesh to avoid remainingDistance exception
        if (warpToNavMeshAtStart && (!agent.enabled || !agent.isOnNavMesh))
        {
            if (NavMesh.SamplePosition(transform.position, out var hit, warpSearchRadius, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                Debug.LogError("[PatrollingAI] Could not place agent on a NavMesh near its start position.", this);
                enabled = false;
                return;
            }
        }

        SetDestinationToCurrent();
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh || waiting)
            return;

        // If we somehow lost the path, try to restore it
        if (!agent.hasPath && !agent.pathPending)
        {
            SetDestinationToCurrent();
            return;
        }

        // Only read remainingDistance when safe
        if (!agent.pathPending &&
            agent.remainingDistance <= Mathf.Max(arriveThreshold, agent.stoppingDistance))
        {
            if (waitAtWaypoint && waitSeconds > 0f)
            {
                waiting = true;
                Invoke(nameof(AdvanceAndSetDestination), waitSeconds);
            }
            else
            {
                AdvanceAndSetDestination();
            }
        }
    }

    private void AdvanceAndSetDestination()
    {
        waiting = false;

        if (!pingPong)
        {
            index = (index + 1) % waypoints.Length;
        }
        else
        {
            if (index == 0) dir = 1;
            else if (index == waypoints.Length - 1) dir = -1;
            index += dir;
        }

        SetDestinationToCurrent();
    }

    private void SetDestinationToCurrent()
    {
        var wp = waypoints[index];
        if (wp == null)
        {
            Debug.LogWarning("[PatrollingAI] Waypoint is null, skipping.", this);
            return;
        }

        if (!agent.SetDestination(wp.position))
        {
            Debug.LogWarning("[PatrollingAI] Failed to set destination. Is the point on the baked NavMesh?", this);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length; i++)
        {
            var wp = waypoints[i];
            if (wp == null) continue;

            Gizmos.DrawSphere(wp.position, 0.15f);

            int next = (i + 1) % waypoints.Length;
            if (i < waypoints.Length - 1 && waypoints[next] != null)
                Gizmos.DrawLine(wp.position, waypoints[next].position);
        }
    }
#endif
}

