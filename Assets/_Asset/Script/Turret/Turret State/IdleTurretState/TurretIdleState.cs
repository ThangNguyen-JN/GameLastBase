using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : StateTurret
{
    public TurretIdleState(StateTurretManager stateTurretManager) : base(stateTurretManager) { }
    // Start is called before the first frame update
    public override void EnterTurretState()
    {
        //stateTurretManager.turretHead.rotation = stateTurretManager.DefaultRotation;
        stateTurretManager.StartIdleTimeout();
    }
    public override void UpdateTurretState()
    {
        if (stateTurretManager.ShouldRotate())
        {
            stateTurretManager.SwitchState(new TurretRotatingState(stateTurretManager));
        }

        if (stateTurretManager.DetectTarget())
        {
            stateTurretManager.SwitchState(new TurretAttackState(stateTurretManager));
        }
    }
    public override void ExitTurretState()
    {

    }
}
