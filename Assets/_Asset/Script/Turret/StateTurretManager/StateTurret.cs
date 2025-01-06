using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateTurret
{
    protected StateTurretManager stateTurretManager;
    public StateTurret (StateTurretManager stateTurretManager)
    {
        this.stateTurretManager = stateTurretManager;
    }

    public abstract void EnterTurretState();
    public abstract void UpdateTurretState();
    public abstract void ExitTurretState();
}
