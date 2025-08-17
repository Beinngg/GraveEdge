using UnityEngine;

public class PlayerOnTrain : MonoBehaviour
{
    private Transform originalParent;

    void Start()
    {
        // Store the original parent so we can reset later
        originalParent = transform.parent;
    }

    void OnTriggerEnter(Collider other) // Changed from Collision to Collider
    {
        if (other.CompareTag("stayOnTrain"))
        {
            // Make the player follow the train
            transform.SetParent(other.transform, true);
        }
    }

    void OnTriggerExit(Collider other) // Changed from Collision to Collider
    {
        if (other.CompareTag("stayOnTrain"))
        {
            // Reset parent when leaving train
            transform.SetParent(originalParent, true);
        }
    }
}
