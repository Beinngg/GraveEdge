using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialPager : MonoBehaviour
{
    [Header("Pages in order (Intro, WASD, Dash, Jump...)")]
    public GameObject[] pages;

    [Header("UI")]
    public Button nextButton;                    // assign your Next button
    public TMP_Text nextButtonLabel;             // optional: label to change to "Finish"
    [Tooltip("Any big background/dim Images that might block clicks.")]
    public Image[] overlayImagesToDisableRaycast;

    [Header("Behaviour")]
    public bool pauseWhileOpen = true;           // pause game during tutorial

    int index = 0;

    void Awake()
    {
        // Make sure we have an EventSystem so clicks work
        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            DontDestroyOnLoad(es);
        }
    }

    void OnEnable()
    {
        // Safety: ensure overlays don’t block the button
        if (overlayImagesToDisableRaycast != null)
            foreach (var img in overlayImagesToDisableRaycast)
                if (img) img.raycastTarget = false;

        // Wire the button
        if (nextButton != null)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(Next);
        }

        // Start on first page
        index = 0;
        ShowOnly(index);
        UpdateNextLabel();

        if (pauseWhileOpen)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnDisable()
    {
        if (pauseWhileOpen)
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (nextButton != null)
            nextButton.onClick.RemoveAllListeners();
    }

    public void Next()
    {
        index++;
        if (index < pages.Length)
        {
            ShowOnly(index);
            UpdateNextLabel();
        }
        else
        {
            // Finished – hide tutorial root
            gameObject.SetActive(false);
        }
    }

    void ShowOnly(int showIndex)
    {
        for (int i = 0; i < pages.Length; i++)
            if (pages[i]) pages[i].SetActive(i == showIndex);
    }

    void UpdateNextLabel()
    {
        if (nextButtonLabel == null) return;
        bool last = (index >= pages.Length - 1);
        nextButtonLabel.text = last ? "Finish" : "Next";
    }
}
