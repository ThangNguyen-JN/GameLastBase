using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTurretState : CharacterState
{
    public IdleTurretState(CharacterStateManager manager) : base(manager) { }

    public override void EnterState()
    {
        Debug.Log("Start Shoot");
        stateManager.gunPlayer.enabled = true;
    }
    public override void UpdateState()
    {
        if (stateManager.moveInZoneResource.IsZoneResource == true)
        {
            stateManager.SwitchState(stateManager.exploitState);
        }

        if (stateManager.IsMoving())
        {
            stateManager.SwitchState(stateManager.movingState);
        }

        if (stateManager.targetManager.HasTargets() && !stateManager.movePlayer.IsMoving())
        {
            GameObject closestTarget = stateManager.targetManager.FindClosestTarget(stateManager.transform.position);

            if (closestTarget != null)
            {
                RotateTowardsTarget(closestTarget.transform);
                stateManager.SwitchState(stateManager.attackState);
            }
        }    
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - stateManager.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            stateManager.transform.rotation = Quaternion.Slerp( stateManager.transform.rotation, targetRotation, stateManager.rotationSpeed * Time.deltaTime);

        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }
}
