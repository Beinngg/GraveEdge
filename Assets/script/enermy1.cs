using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets; // <-- Add this line

public class enermy1 : MonoBehaviour
{
    [SerializeField] private float turretRange = 30f;
    [SerializeField] private float turretRotationSpeed = 5f;

    private Transform PlayerTransform;
    private shooting CurrentShooting;
    private float fireRate;
    private float fireRateDelta;
    private void Start()
    {
        PlayerTransform = FindObjectOfType<FirstPersonController>()?.transform;
        if (PlayerTransform == null)
        {
            Debug.LogError("FirstPersonController not found in the scene!");
            return;
        }

        CurrentShooting = GetComponentInChildren<shooting>();
        if (CurrentShooting == null)
        {
            Debug.LogError("No 'shooting' component found in " + gameObject.name + " or its children!");
            return;
        }
        fireRate = CurrentShooting.RateOffFire();
    }
    private void Update()
    {
        if (PlayerTransform == null)
        {
            return;
        }
        Vector3 playerGroundPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        if (Vector3.Distance(transform.position, playerGroundPos) > turretRange)
        {
            return;
        }
        Vector3 playerDirection = playerGroundPos - transform.position;
        float turretRotationStep = turretRotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, turretRotationStep, 0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
        fireRateDelta -= Time.deltaTime;
        if (fireRateDelta <= 0f)
        {
            CurrentShooting.Fire();
            fireRateDelta = fireRate;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
   
    
}

