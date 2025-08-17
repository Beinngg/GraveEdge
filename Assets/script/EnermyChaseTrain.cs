using UnityEngine;

[ExecuteAlways]
public class EnemyRailFollower : MonoBehaviour
{
    public Transform train;

    [Header("Local-space offset on the train")]
    // Tune these in the Inspector until the bot sits where you want
    public Vector3 localOffset = new Vector3(-6f, 0f, 0f); // e.g. -X = behind if your mesh points +X
    public Vector3 localEulerOffset = Vector3.zero;

    [Header("Smoothing")]
    public float smoothTime = 0.15f;
    public float rotateLerp = 0.15f;

    Vector3 vel;

    void LateUpdate()
    {
        if (!train) return;

        // Convert local offset to world, so it works no matter which axis your mesh uses
        Vector3 targetPos = train.TransformPoint(localOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);

        Quaternion targetRot = train.rotation * Quaternion.Euler(localEulerOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateLerp);
    }

    void OnDrawGizmos()
    {
        if (!train) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(train.TransformPoint(localOffset), 0.25f);
    }
}
