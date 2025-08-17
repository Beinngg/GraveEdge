using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage = 100000000000f; // Amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        // Prefer component-based check (works even if tag is wrong/missing)
        if (other.TryGetComponent<HealthSystem>(out var enemy))
        {
            enemy.TakeDamage(damage);
            return;
        }

        // Optional: if you also want to gate by tag (change "Enemy" to your actual tag)
        // if (other.CompareTag("Enemy") && other.TryGetComponent<HealthSystem>(out var enemyByTag))
        // {
        //     enemyByTag.TakeDamage(damage);
        // }
    }
}
