using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChasingState : IZombieState
{
    public void EnterState ( ZomManager zombie )
    {

        zombie.anim.SetBool("isAttacking", false);
    }

    public void UpdateState (ZomManager zombie ) 
    {
        if (zombie.target == null || zombie.canSeePlayer == false)
        {
            zombie.ChangeState(new ZombiePatrolState());
            return;
        }
        zombie.movement.MoveToPlayer(zombie.target.position);

        float distanceToPlayer = Vector3.Distance(zombie.transform.position, zombie.target.position);

        if (/*!zombie.movement.agent.pathPending*/ distanceToPlayer <= zombie.movement.agent.stoppingDistance)
        {
            zombie.ChangeState(new ZombieAttackState());
        }



    }

    public void ExitState (ZomManager zombie ) 
    { 
    
    }
}
