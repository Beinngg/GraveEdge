using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    public Transform steadyAnchor;
    public Transform slashAnchor;
    public Animator playerAnimator;
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (steadyAnchor == null || slashAnchor == null || playerAnimator == null) return;

        // Choose target anchor based on animation
        Transform target = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slashing") ? slashAnchor : steadyAnchor;

        // Smoothly follow position
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * smoothSpeed);

        // Smoothly follow ONLY Y-axis (yaw)
        Vector3 currentEuler = transform.rotation.eulerAngles;
        Vector3 targetEuler = target.rotation.eulerAngles;

        float smoothYaw = Mathf.LerpAngle(currentEuler.y, targetEuler.y, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothYaw, 0f); // keep pitch from CameraPivot
    }
}