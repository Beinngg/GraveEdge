using UnityEngine;

public class MoveZForever : MonoBehaviour
{
    public float speed = 5f;          // Movement speed
    public bool moveNegative = true;  // True = move -Z, False = +Z

    void Update()
    {
        float direction = moveNegative ? -1f : 1f;
        transform.Translate(0, 0, speed * Time.deltaTime * direction);
    }
}
