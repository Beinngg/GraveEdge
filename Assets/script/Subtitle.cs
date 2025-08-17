using UnityEngine;
using System.Collections;
using TMPro;

public class Subtitle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textComponent;   // Assign in Inspector

    [Header("Content")]
    [TextArea] public string[] lines;

    [Header("Timing")]
    public float charDelay = 0.03f;         // seconds per character (unscaled)
    public float linePause = 1.0f;          // pause between lines (unscaled)
    public bool autoStartOnEnable = true;
    public bool autoAdvance = true;

    private int index = 0;
    private Coroutine typingCo;

    void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    void OnEnable()
    {
        if (autoStartOnEnable) StartSubtitle();
    }

    public void StartSubtitle()
    {
        if (textComponent == null)
        {
            Debug.LogError("[Subtitle] No TextMeshProUGUI assigned.");
            return;
        }
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("[Subtitle] No lines set.");
            return;
        }

        index = 0;
        textComponent.gameObject.SetActive(true);
        textComponent.text = string.Empty;

        if (typingCo != null) StopCoroutine(typingCo);
        typingCo = StartCoroutine(TypeLine());
    }

    public void ShowFirstLineInstant()
    {
        // Handy for testing wiring: call this to force-show text
        if (textComponent == null || lines == null || lines.Length == 0) return;
        textComponent.text = lines[0];
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        string line = lines[index];

        foreach (char c in line)
        {
            textComponent.text += c;
            yield return WaitUnscaled(charDelay);
        }

        if (autoAdvance)
        {
            yield return WaitUnscaled(linePause);
            NextLine();
        }
    }

    public void NextLine()
    {
        if (typingCo != null) StopCoroutine(typingCo);

        index++;
        if (index < lines.Length)
        {
            typingCo = StartCoroutine(TypeLine());
        }
        else
        {
            // Done
            // Optionally hide or keep the last line
            // textComponent.text = string.Empty;
        }
    }

    // Unscaled wait so subtitles work even if Time.timeScale == 0
    private static IEnumerator WaitUnscaled(float seconds)
    {
        float end = Time.unscaledTime + Mathf.Max(0f, seconds);
        while (Time.unscaledTime < end) yield return null;
    }
}
