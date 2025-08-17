/*
* Author: YourName
* Date: 2025-08-13
* Description: Plays a specific animation when a button is clicked.
*/

using UnityEngine;

public class PlayAnimationOnClick : MonoBehaviour
{
    public Animator animator;         // Animator on your image
    public string animationName;      // Animation to play on click

    // This function will be called when the button is clicked
    public void PlayAnimation()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Try to auto-find Animator
        }

        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName);
        }
    }
}
