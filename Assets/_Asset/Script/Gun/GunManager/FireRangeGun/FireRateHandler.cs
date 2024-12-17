using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateHandler : MonoBehaviour
{
    [SerializeField]private float fireRate;
    private float lastFireTime;

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = Mathf.Max(value, 0.01f); }
    }

    public bool CanShoot()
    {
        return Time.time - lastFireTime >= fireRate;
    }

    public void ResetFireTime()
    {
        lastFireTime = Time.time;
    }
}
