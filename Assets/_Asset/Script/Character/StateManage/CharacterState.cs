using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    protected CharacterStateManager stateManager;

    public CharacterState (CharacterStateManager manager)
    {
        stateManager = manager;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
