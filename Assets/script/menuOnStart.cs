/*
* Author: YourName
* Date: 2025-08-13
* Description: Plays an animation automatically when the game starts.
*/

using UnityEngine;

public class PlayAnimationOnStart : MonoBehaviour
{
    public Animator animator;         // Reference to Animator component
    public string animationName;      // Name of the animation to play

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Auto-assign if possible
        }

        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName);
        }
    }
}
