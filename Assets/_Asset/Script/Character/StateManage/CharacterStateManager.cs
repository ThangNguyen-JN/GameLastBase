using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    public IdleTurretState idleState { get; private set; }
    public MovingState movingState { get; private set; }
    public AttackState attackState { get; private set; }
    public ExploitState exploitState { get; private set; }
    public CharacterState currentState { get; private set; }

    public MovePlayer movePlayer;
    public MoveInZoneResource moveInZoneResource;
    public GunPlayer gunPlayer;
    public TargetManager targetManager;
    public Animator animator;

    public float rotationSpeed = 5f;
    public bool playerInZoneResource = false;

    public new void StartCoroutine(IEnumerator coroutine)
    {
        base.StartCoroutine(coroutine);
    }

    private void Start()
    {
        //Khoi tao cac trang thai
        idleState = new IdleTurretState(this);
        movingState = new MovingState(this);
        attackState = new AttackState(this);
        exploitState = new ExploitState(this);

        //Bat dau o trang thai Idle
        currentState = idleState;
        currentState.EnterState();

        //moveInZoneResource.PlayerInZoneResource += CheckInZoneResource;
        //CheckInZoneResource(playerInZoneResource);
    }

    private void Update()
    {
        currentState.UpdateState();
        DrawRayToClosestTarget();
    }

    public void SwitchState(CharacterState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public bool IsMoving()
    {
        return movePlayer.joystick.Direction.sqrMagnitude > 0;
    }
    //public void CheckInZoneResource (bool playInZone)
    //{
    //    if (moveInZoneResource.IsZoneResource != playInZone)
    //    {
    //        moveInZoneResource.IsZoneResource = playInZone;
    //    }
    //}

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
