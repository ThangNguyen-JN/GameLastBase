using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGunSystem : GunBase
{
    public DamageTurretGun damageTurretGun;
    public TargetManager targetManager;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform parentBullet;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private GameObject currentTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame

    public void DealDamage(GameObject target)
    {
        if (damageTurretGun != null && target != null)
        {
            damageTurretGun.DealDamage(target);
        }
    }

    public void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public override void Shoot()
    {
        if (targetManager == null)
        {
            Debug.LogWarning("Target manager is not assigned!");
            return;
        }

        if (targetManager != null)
        {
            currentTarget = targetManager.FindClosestTarget(transform.position);

            if (currentTarget != null)
            {
                Debug.Log("Shooting at target");
                Vector3 fireDirection = (currentTarget.transform.position - firePoint.position).normalized;
                fireDirection.y = 0;
                fireDirection.Normalize();

                GameObject bulletTurret = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity, parentBullet);
                BulletTurret bulletScript = bulletTurret.GetComponent<BulletTurret>();

                if (bulletScript != null)
                {
                    bulletScript.Initialize(fireDirection, damageTurretGun.Damage);
                    Debug.Log("Bullet fired");
                }
            }
        }

    }


}
