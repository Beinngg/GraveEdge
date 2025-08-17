using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [Header("UI")]
    public GameObject finishUIGroup; // The whole UI group to show

    [Header("Player Control Freeze (optional)")]
    [SerializeField] private MonoBehaviour playerController; // drag your movement script here

    private bool isFinishUIOpen = false;

    private void Start()
    {
        // Ensure it starts hidden
        if (finishUIGroup != null)
            finishUIGroup.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFinishUIOpen)
        {
            ShowFinishUI();
        }
    }

    private void ShowFinishUI()
    {
        if (finishUIGroup != null)
            finishUIGroup.SetActive(true);

        // Freeze time
        Time.timeScale = 0f;

        // Freeze player movement (optional)
        if (playerController != null)
            playerController.enabled = false;

        // Enable mouse for UI
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isFinishUIOpen = true;
    }

    // Call this from your UI button OnClick event
    public void CloseFinishUI()
    {
        if (finishUIGroup != null)
            finishUIGroup.SetActive(false);

        // Resume time
        Time.timeScale = 1f;

        // Unfreeze player
        if (playerController != null)
            playerController.enabled = true;

        // Lock mouse again
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isFinishUIOpen = false;
    }
}
