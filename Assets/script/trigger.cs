using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField] GameObject enermyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");

            // Find all enemies with the same name as the prefab
            enermy2[] allEnemies = FindObjectsOfType<enermy2>();

            foreach (var enemy in allEnemies)
            {
                if (enemy.name.Contains(enermyPrefab.name))
                {
                    enemy.MoveBackSmoothly();
                    break; // Only affect one
                }
            }

            Destroy(gameObject);
        }
    }
}
