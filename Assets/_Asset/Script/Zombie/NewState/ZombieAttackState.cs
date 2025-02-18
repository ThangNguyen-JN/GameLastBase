using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : IZombieState
{
    public void EnterState(ZomManager zombie)
    {
        Debug.Log("Zombie started ATTACKING");
        zombie.anim.SetBool("isAttacking", true);
    }    

    public void UpdateState (ZomManager zombie)
    {

       
        float distanceToPlayer = Vector3.Distance(zombie.transform.position, zombie.target.position);

        if (distanceToPlayer >= zombie.stopDistance +0.5f)
        {
            zombie.ChangeState(new ZombieChasingState()); // quay lai trang thai duoi theo
        }
        RotateTowardsTarget(zombie);

    }

    public void ExitState (ZomManager zombie) 
    {
        Debug.Log("Zombie exited ATTACKING");
    }

    private void RotateTowardsTarget(ZomManager zombie)
    {
        Vector3 direction = (zombie.target.position - zombie.transform.position).normalized;
        direction.y = 0; // 

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
