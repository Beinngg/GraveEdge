using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator animator;    
    [SerializeField] private string triggerName = "isSlashing";
    [SerializeField] private Collider weaponCollider;

    void Start()
    {
        // Ensure animator is assigned
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        // Disable weapon collider at start
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (animator != null)
                animator.SetTrigger(triggerName);
        }
    }

    // Called via animation event
    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = true;
    }

    // Called via animation event
    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }
}
