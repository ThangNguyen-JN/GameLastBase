using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : CharacterState
{
    public MovingState(CharacterStateManager manager) : base(manager) { }

    public override void EnterState()
    {
        stateManager.gunPlayer.enabled = false; 
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
    }
}
