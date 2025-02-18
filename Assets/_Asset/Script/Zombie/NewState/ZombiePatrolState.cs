using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrolState : IZombieState
{
    public bool isMoving = false;
    public void EnterState(ZomManager zombie)
    {
        isMoving = true;
        zombie.StartCoroutine(PatrolRoutine(zombie));
    }

    public void UpdateState(ZomManager zombie)
    {
        if (zombie.canSeePlayer == true)
        {
            zombie.ChangeState(new ZombieChasingState());
        }
    }

    public void ExitState(ZomManager zombie)
    {
    }

    private IEnumerator PatrolRoutine(ZomManager zombie)
    {
        while (!zombie.canSeePlayer)
        {
            Vector3 randomPoint = zombie.GetRandomPatrolPoint();
            zombie.movement.MoveToPlayer(randomPoint);
            yield return new WaitForSeconds(zombie.patrolWaitTime);

            // di chuyen trong mot khoang thoi gian ngau nhien
            float patrolTime = Random.Range(2f, 8f);

            zombie.movement.StopMoving();
            yield return new WaitForSeconds(3f);
        }
    }
}
