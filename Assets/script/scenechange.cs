using UnityEngine;

public class scenechange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to change the scene or load a new level
            SceneController.instance.NextScene();
        }
    }
}
