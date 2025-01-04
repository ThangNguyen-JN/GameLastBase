using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CharacterState
{
    public MovingState(CharacterStateManager manager) : base(manager) { }

    public override void EnterState()
    {
        Debug.Log("Player is moving");
        stateManager.playerGun.enabled = false; 
    }
    public override void UpdateState()
    {
        if (!stateManager.IsMoving())
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Moving State");
    }
}
