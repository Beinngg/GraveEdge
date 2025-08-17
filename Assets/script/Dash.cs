using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using System.Collections;

public class Dashing : MonoBehaviour
{
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.Q;

    [Header("UI")]
    public Image dashFill;              // Radial fill image
    public CanvasGroup dashGroup;       // Optional: fade/flash
    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(1f,1f,1f,0.4f);

    private FirstPersonController controller;
    private float originalWalkSpeed;
    private float originalSprintSpeed;

    // state
    private bool isDashing = false;
    private bool canDash = true;
    private float cooldownRemaining = 0f;

    void Start()
    {
        controller = GetComponent<FirstPersonController>();
        if (controller == null)
        {
            Debug.LogError("FirstPersonController not found on this GameObject.");
            enabled = false;
            return;
        }

        originalWalkSpeed = controller.walkSpeed;
        originalSprintSpeed = controller.sprintSpeed;

        // UI init
        if (dashFill) dashFill.fillAmount = 1f; // full = ready
        SetUIReady(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash && !isDashing)
            StartCoroutine(Dash());

        // tick cooldown
        if (!canDash)
        {
            cooldownRemaining = Mathf.Max(0f, cooldownRemaining - Time.deltaTime);
            if (dashFill)
                dashFill.fillAmount = 1f - (cooldownRemaining / Mathf.Max(0.0001f, dashCooldown));

            if (cooldownRemaining <= 0f)
            {
                canDash = true;
                SetUIReady(true);
            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        cooldownRemaining = dashCooldown;

        // UI: set to 0 and grey out, then flash
        if (dashFill) dashFill.fillAmount = 0f;
        SetUIReady(false);
        StartCoroutine(FlashGroup(0.85f, 0.2f)); // quick “whoosh” flash

        // perform dash
        controller.walkSpeed = dashSpeed;
        controller.sprintSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);

        // restore speeds
        controller.walkSpeed = originalWalkSpeed;
        controller.sprintSpeed = originalSprintSpeed;
        isDashing = false;
        // cooldown continues in Update()
    }

    private void SetUIReady(bool ready)
    {
        if (dashGroup)
            dashGroup.alpha = ready ? 1f : 0.6f;

        if (dashFill)
            dashFill.color = ready ? readyColor : cooldownColor;
    }

    private IEnumerator FlashGroup(float peak = 0.9f, float time = 0.15f)
    {
        if (!dashGroup) yield break;

        float t = 0f;
        float start = dashGroup.alpha;
        while (t < time)
        {
            t += Time.deltaTime;
            float k = t / time;
            // up then down
            float v = Mathf.Sin(k * Mathf.PI);
            dashGroup.alpha = Mathf.Lerp(start, peak, v);
            yield return null;
        }
        dashGroup.alpha = Mathf.Clamp01(start);
    }
}
