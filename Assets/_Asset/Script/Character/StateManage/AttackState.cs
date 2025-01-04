using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState
{
    private bool hasRotated = false;
    public AttackState(CharacterStateManager manager) : base(manager) { }
    // Start is called before the first frame update
    public override void EnterState()
    {
        stateManager.animator.SetBool("isAttack", true);
        stateManager.playerGun.enabled = true;
        hasRotated = false;
    }

    public override void UpdateState()
    {
        if (!stateManager.targetManager.HasTargets())
        {
            stateManager.SwitchState(stateManager.idleState);
            return;
        }

        if (stateManager.IsMoving())
        {
            stateManager.SwitchState(stateManager.movingState);
            return;
        }

        GameObject closestTarget = stateManager.targetManager.FindClosestTarget(stateManager.transform.position);
        if (closestTarget != null) 
        {

            RotateTowardsTarget(closestTarget.transform);
            if (!hasRotated && IsRotationComplete(closestTarget.transform))
            {
                hasRotated = true;
            }

            if (hasRotated)
            {
                stateManager.StartCoroutine(WaitAndShoot());
            }
        }
    }

    private IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(0.5f);

        if (stateManager.playerGun.CanShoot())
        {
            stateManager.playerGun.Shoot();
            stateManager.playerGun.ResetFireTime();
        }
    }


    public override void ExitState() 
    {
        stateManager.animator.SetBool("isAttack", false);
        stateManager.playerGun.enabled = false;
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - stateManager.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero) 
        { 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, targetRotation, stateManager.rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsRotationComplete(Transform target)
    {
        float angle = Vector3.Angle(stateManager.transform.forward, target.position - stateManager.transform.position);
        return angle < 5f;
    }
}
