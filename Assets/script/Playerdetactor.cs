using UnityEngine;

public class Playerdetactor : MonoBehaviour
{
    static public bool found = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            found = true;
            // Add logic to handle player detection
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            found = false;
            // Add logic to handle player exit
        }
    }
}
