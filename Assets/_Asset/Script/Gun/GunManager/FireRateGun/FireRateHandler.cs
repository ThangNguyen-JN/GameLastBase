using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateHandler : MonoBehaviour
{
    [SerializeField]private float fireRate;
    [SerializeField] private int burstSize = 5;
    [SerializeField] private float cooldownTime = 0.5f;

    private float lastFireTime;
    private int shotsFired;
    private bool isCoolingDown;

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = Mathf.Max(value, 0.01f); }
    }

    public bool CanShoot()
    {
        if (isCoolingDown)
        {
            if (Time.time - lastFireTime >= cooldownTime)
            {
                isCoolingDown = false;
                shotsFired = 0;
            }
            else
            {
                return false;
            }
        }

        if (shotsFired >= burstSize)
        {
            isCoolingDown = true;
            lastFireTime = Time.time;
            return false;
        }
        return Time.time - lastFireTime >= fireRate;
    }

    public void ResetFireTime()
    {
        lastFireTime = Time.time;
        shotsFired++;
    }
}
