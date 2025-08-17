using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float damage = 10f;
    [SerializeField] float maxLifetime = 5f; // Time before bullet is destroyed
    private Vector3 startPosition; // Where the bullet was fired from
    [SerializeField] float maxDistance = 50f; // Max range before destroy

    private void Start()
    {
        // Save where the bullet started
        startPosition = transform.position;

        // Option 1: Auto destroy after some time (backup)
        Destroy(gameObject, maxLifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

        // Check if bullet has gone beyond maxDistance
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.ModifyHealth(-damage);
            }

            // Destroy bullet after hitting player
            Destroy(gameObject);
        }
        else if (!other.isTrigger) // If it hits something solid (like a wall)
        {
            Destroy(gameObject);
        }
    }
}
