using UnityEngine;

public class UIUnlockCursor : MonoBehaviour
{
    [Header("Pause while UI is open")]
    public bool pauseGame = true;

    void OnEnable()
    {
        if (pauseGame) Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;  // unlock
        Cursor.visible = true;                   // show
    }

    void OnDisable()
    {
        if (pauseGame) Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; // lock for FPS
        Cursor.visible = false;                   // hide
    }
}
