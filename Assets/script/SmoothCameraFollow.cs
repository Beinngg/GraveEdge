using UnityEngine;

public class JointFollowHead : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform headBone;       // Drag your guy's head bone here
    public Vector3 offset = Vector3.zero;

    [Header("Smoothing")]
    public float positionSmooth = 12f;
    public float rotationSmooth = 12f;

    void LateUpdate()
    {
        if (headBone == null) return;

        // Target position and rotation from head bone
        Vector3 targetPos = headBone.position + offset;
        Quaternion targetRot = headBone.rotation;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * positionSmooth);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSmooth);
    }
}
