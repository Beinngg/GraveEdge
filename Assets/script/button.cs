using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string newGameLevel1 = "1"; // Name of the scene to load

    public void LoadNewGame()
    {
        // Make sure game is unpaused
        Time.timeScale = 1f;

        // Lock and hide cursor for FPS movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Load the specified scene
        SceneManager.LoadScene(newGameLevel1);
    }
}
