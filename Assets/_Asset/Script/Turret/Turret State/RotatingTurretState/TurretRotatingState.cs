using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotatingState : StateTurret
{

    private float rotationSpeed = 30f;
    private float randomAngle;
    private bool isRotating = false;
    public TurretRotatingState(StateTurretManager stateTurretManager) : base(stateTurretManager) { }
    // Start is called before the first frame update
    public override void EnterTurretState()
    {
        randomAngle = Random.Range(0f, 360f);

        Debug.Log($"Random Angle: {randomAngle} ");
        isRotating = true;
    }
    public override void UpdateTurretState()
    {
        if (isRotating == true)
        {
            RotateToRandomAngle();
        }
        if (stateTurretManager.DetectTarget())
        {
            stateTurretManager.SwitchState(new TurretAttackState(stateTurretManager));
        }
    }
    public override void ExitTurretState()
    { 
        isRotating = false;
    }

    private void RotateToRandomAngle()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, randomAngle, 0f) * stateTurretManager.DefaultRotation;

        stateTurretManager.turretHead.rotation = Quaternion.RotateTowards(stateTurretManager.turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(stateTurretManager.turretHead.rotation, targetRotation) < 0.1f ) 
        {
            isRotating = false;
            stateTurretManager.turretHead.rotation = targetRotation;
            stateTurretManager.SwitchState(new TurretIdleState(stateTurretManager));
        }
    }

    //private IEnumerator WaitBeforeSwitchState()
    //{
    //    yield return new WaitForSeconds(2f); 
    //    stateTurretManager.SwitchState(new TurretIdleState(stateTurretManager));
    //}



}
