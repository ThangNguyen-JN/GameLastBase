using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTurretManager : MonoBehaviour
{
    private StateTurret currentTurretState;
    public TurretIdleState turretIdleState { get; set; }
    public TurretRotatingState turretRotatingState { get; set; }
    public TurretAttackState turretAttackState { get; set; }

    private bool isIdleTimeout = false;
    public Transform turretHead;

    public TargetManager targetManager;
    public TurretGunSystem turretGunSystem;
    public Quaternion DefaultRotation => Quaternion.identity;

    public new void StartCoroutine(IEnumerator coroutine)
    {
        base.StartCoroutine(coroutine);
    }

    private void Start()
    {
        turretIdleState = new TurretIdleState(this);
        turretRotatingState = new TurretRotatingState(this);
        turretAttackState = new TurretAttackState(this);

        currentTurretState = turretIdleState;
        currentTurretState.EnterTurretState();
    }

    private void Update()
    {
        currentTurretState.UpdateTurretState();
        DrawRayToClosestTarget();
    }

    public void SwitchState(StateTurret newState)
    {
        currentTurretState.ExitTurretState();
        currentTurretState = newState;
        currentTurretState.EnterTurretState();
    }

    public bool ShouldRotate()
    {
        //logic dieu kien de quay
        return isIdleTimeout;
    }

    public bool DetectTarget()
    {
        // logic phat hien muc tieu
        return targetManager.HasTargets(); ; 
    }

    public void StartIdleTimeout()
    {
        StartCoroutine(IdleTimeoutCoroutine());
    }

    private IEnumerator IdleTimeoutCoroutine()
    {
        isIdleTimeout = false; 
        yield return new WaitForSeconds(2f); 
        isIdleTimeout = true;
    }

    //Raycast to detect closest target
    private void DrawRayToClosestTarget()
    {
        if (targetManager.HasTargets())
        {
            GameObject closestTarget = targetManager.FindClosestTarget(transform.position);

            if (closestTarget != null)
            {
                Debug.DrawLine(
                    transform.position + Vector3.up,
                    closestTarget.transform.position + Vector3.up,
                    Color.red
                );
            }
        }
    }
}
