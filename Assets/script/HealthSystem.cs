using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 10f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " is destroyed!");
            Destroy(gameObject);
        }
    }
}
