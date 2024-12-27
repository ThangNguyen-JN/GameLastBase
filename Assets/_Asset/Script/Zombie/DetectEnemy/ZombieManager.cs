using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public ZombieMovementWithBoxTrigger movementSystem;
    public ZombieChaseSystem chaseSystem;
    public DetectEnemySystem detectSystem;

    private void Start()
    {
        detectSystem.onEnemyDetected += HandleEnemyDetected;
        detectSystem.onEnemyLost += HandleEnemyLost;
    }

    private void HandleEnemyDetected(Transform enemy)
    {
        movementSystem.enabled = false; 
        chaseSystem.StartChasing(enemy); 
    }

    private void HandleEnemyLost()
    {
        chaseSystem.StopChasing(); 
        movementSystem.enabled = true; 
    }
}
