using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public Image damageOverlay;
    public float overlayDuration = 1f;

    [Header("Death UI")]
    public GameObject deathUIGroup;

    [Header("Regen Settings")]
    public float regenDelay = 5f;    // seconds without damage before regen starts
    public float regenDuration = 1f; // time to fully heal once regen starts

    private float currentHealth = 100f;
    private Coroutine overlayCoroutine;
    private bool isDead = false;
    private float lastDamageTime;
    private float regenRate; // calculated from regenDuration

    void Start()
    {
        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0);

        if (deathUIGroup != null)
            deathUIGroup.SetActive(false);

        lastDamageTime = Time.time;

        // Calculate regen speed
        regenRate = 100f / regenDuration;
    }

    void Update()
    {
        if (!isDead && Time.time - lastDamageTime >= regenDelay && currentHealth < 100f)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, 100f);

            if (healthBar != null)
                healthBar.fillAmount = currentHealth / 100f;
        }
    }

    public void ModifyHealth(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, 100f);

        if (healthBar != null)
            healthBar.fillAmount = currentHealth / 100f;

        if (amount < 0)
        {
            lastDamageTime = Time.time;
            ShowDamageOverlay();
        }

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            ShowDeathUI();
        }
    }

    private void ShowDamageOverlay()
    {
        if (damageOverlay == null) return;

        damageOverlay.color = new Color(1, 0, 0, 0.5f);

        if (overlayCoroutine != null)
            StopCoroutine(overlayCoroutine);

        overlayCoroutine = StartCoroutine(HideOverlayAfterDelay());
    }

    private IEnumerator HideOverlayAfterDelay()
    {
        yield return new WaitForSeconds(overlayDuration);
        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0);
    }

    private void ShowDeathUI()
    {
        if (deathUIGroup != null)
        {
            deathUIGroup.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
