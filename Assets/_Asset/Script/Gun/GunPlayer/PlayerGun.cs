using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : GunBase
{
    private TargetFinder targetFinder;
    private GameObject currentTarget;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetFinder = GetComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetFinder != null) 
        {
            currentTarget = targetFinder.FindClosestTarget(transform.position);

            if (currentTarget != null && CanShoot())
            {
                Shoot();
                ResetFireTime();
            }
        }

    }

   
    public override void Shoot()
    {
       if (currentTarget!= null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Vector3 direction = (currentTarget.transform.position - firePoint.position).normalized;
            bulletScript.Initialize(direction); // khoi tao bullet voi huong di toi muc tieu

            DealDamage(currentTarget);
        }
    }
}
