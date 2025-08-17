using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow; // The object you want to appear
    [SerializeField] private string playerTag = "Player"; // Tag for your player object

    private void Start()
    {
        if (objectToShow != null)
            objectToShow.SetActive(false); // Hide it at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (objectToShow != null)
                objectToShow.SetActive(true); // Show when player enters
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (objectToShow != null)
                objectToShow.SetActive(false); // Hide when player exits
        }
    }
}
