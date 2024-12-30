using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public ZombieMovementWithBoxTrigger movementSystem;

    public ZombieChaseSystem chaseSystem;
    public DetectEnemySystem chaseDetectSystem;
    public DetectEnemySystem attackDetectSystem;
    public ZombieAttackSystem attackSystem;

    private ZombieState currentState = ZombieState.Idle;
    private void Start()
    {
        chaseDetectSystem.onEnemyDetected += HandleChaseDetected;
        chaseDetectSystem.onEnemyLost += HandleChaseLost;

        attackDetectSystem.onEnemyDetected += HandleAttackDetected;
        attackDetectSystem.onEnemyLost += HandleAttackLost;

        attackSystem.onDamageDealt += HandleDamageDealt;
    }

    private void HandleChaseDetected(Transform enemy)
    {
        if (currentState != ZombieState.Attacking)
        { 
            movementSystem.enabled = false;
            chaseSystem.StartChasing(enemy);
            currentState = ZombieState.Chasing;
        }
    }

    private void HandleChaseLost()
    {
        if (currentState == ZombieState.Chasing)
        {
            chaseSystem.StopChasing();
            movementSystem.enabled = true;
            currentState = ZombieState.Idle;
        }
    }

    private void HandleAttackDetected(Transform enemy)
    {
        chaseSystem.StopChasing();
        attackSystem.StartAttacking(enemy);
        currentState = ZombieState.Attacking;
    }

    private void HandleAttackLost()
    {
        attackSystem.StopAttacking();
        currentState = ZombieState.Chasing;
        chaseSystem.StartChasing(attackSystem.GetCurrentTarget());
    }

    private void HandleDamageDealt(int damage)
    {
        
    }
}
