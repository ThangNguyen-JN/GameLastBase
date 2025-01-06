using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretAttackState : StateTurret
{
    private float rotationSpeed = 200f;
    private GameObject currentTarget;
    private bool isShooting = false;
    public TurretAttackState(StateTurretManager stateTurretManager) : base(stateTurretManager) { }
    // Start is called before the first frame update
    public override void EnterTurretState()
    {
        currentTarget = stateTurretManager.targetManager.FindClosestTarget(stateTurretManager.transform.position);
        stateTurretManager.turretGunSystem.enabled = true;

    }
    public override void UpdateTurretState()
    {
        if (currentTarget == null || stateTurretManager.targetManager.HasTargets() == false)
        {
            stateTurretManager.targetManager.RemoveNullTargets();
            stateTurretManager.SwitchState(new TurretIdleState(stateTurretManager));
            Debug.Log("No Target");
            return;
        }

        currentTarget = stateTurretManager.targetManager.FindClosestTarget(stateTurretManager.transform.position);
        if (currentTarget != null) 
        {
            RotateToTarget(currentTarget.transform.position);

            if (!isShooting && IsRotationComplete(currentTarget.transform))
            {
                isShooting = true;
                stateTurretManager.StartCoroutine(ShootAtTarget());
            }

        } 
    }

    private IEnumerator ShootAtTarget()
    {
        while (isShooting)
        {
            if (stateTurretManager.turretGunSystem.CanShoot())
            {
                stateTurretManager.turretGunSystem.Shoot();
                stateTurretManager.turretGunSystem.ResetFireTime();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public override void ExitTurretState()
    {
        isShooting = false;
        currentTarget = null;
    }

    private void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - stateTurretManager.turretHead.position;
        direction.y = 0; 

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        stateTurretManager.turretHead.rotation = Quaternion.RotateTowards(stateTurretManager.turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsRotationComplete(Transform target)
    {
        Vector3 direction = target.position - stateTurretManager.turretHead.position;
        direction.y = 0;

        float angle = Vector3.Angle(stateTurretManager.turretHead.forward, direction);
        return angle < 1f;
    }
}
