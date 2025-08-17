using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float rateOffFire = 1f;
    [SerializeField] Transform gunPoint;

    public float RateOffFire()
    {
        return rateOffFire;
    }
    public void Fire()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
