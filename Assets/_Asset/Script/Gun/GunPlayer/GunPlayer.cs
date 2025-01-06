using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : GunBase
{
    public TargetManager targetManager;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private GameObject currentTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame

    public override void Shoot()
    {
        if (targetManager != null)
        {
            currentTarget = targetManager.FindClosestTarget(transform.position);

            if (currentTarget != null)
            {
                Debug.Log("Shooting at target");
                Vector3 fireDirection = firePoint.forward;
                fireDirection.y = 0;
                fireDirection.Normalize();

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();

                if (bulletScript != null)
                {
                    bulletScript.Initialize(fireDirection, damageHandler.Damage);
                    Debug.Log("Bullet fired");
                }
            }
        }
    }
}
