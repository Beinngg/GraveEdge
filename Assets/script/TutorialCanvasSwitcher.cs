using UnityEngine;

public class TutorialCanvasSwitcher : MonoBehaviour
{
    // Session-only memory (resets when the app restarts)
    private static bool tutorialSeenThisSession = false;

    [Header("Assign UI Roots")]
    [SerializeField] private GameObject introPanel;    // first picture
    [SerializeField] private GameObject controlsPanel; // second picture
    [SerializeField] private GameObject canvasRoot;    // whole canvas (turn off to close)

    [Header("Optional: freeze player while tutorial open")]
    [SerializeField] private MonoBehaviour playerController; // drag your FirstPersonController here

    private enum State { Intro, Controls, Hidden }
    private State state = State.Intro;

    void Start()
    {
        if (canvasRoot == null) canvasRoot = gameObject;

        // If we already showed it this session (e.g., after death/scene reload), skip
        if (tutorialSeenThisSession)
        {
            if (playerController) playerController.enabled = true;
            ToggleCursor(false);
            Destroy(canvasRoot);
            enabled = false;
            return;
        }

        ShowIntro();
    }

    void Update()
    {
        if (state == State.Hidden) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (state == State.Intro)      ShowControls();
            else if (state == State.Controls) CloseAndDestroy();
        }
    }

    private void ShowIntro()
    {
        canvasRoot.SetActive(true);
        if (introPanel) introPanel.SetActive(true);
        if (controlsPanel) controlsPanel.SetActive(false);
        if (playerController) playerController.enabled = false; // freeze player
        ToggleCursor(true);
        state = State.Intro;
    }

    private void ShowControls()
    {
        if (introPanel) introPanel.SetActive(false);
        if (controlsPanel) controlsPanel.SetActive(true);
        state = State.Controls;
    }

    private void CloseAndDestroy()
    {
        // mark as seen for this session only
        tutorialSeenThisSession = true;

        if (playerController) playerController.enabled = true;
        ToggleCursor(false);

        if (canvasRoot) Destroy(canvasRoot); else Destroy(gameObject);
        state = State.Hidden;
        enabled = false;
    }

    private void ToggleCursor(bool on)
    {
        Cursor.visible = on;
        Cursor.lockState = on ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
