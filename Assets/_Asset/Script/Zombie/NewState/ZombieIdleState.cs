using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : IZombieState
{
    public void EnterState(ZomManager zombie)
    {
        zombie.anim.SetBool("isAttacking", false);
        zombie.anim.SetBool("isMoving", false);
        zombie.movement.StopMoving();
        zombie.StartCoroutine(WaitBeforeIdle(zombie));
    }

    public void UpdateState (ZomManager zombie) 
    {
        if (zombie.canSeePlayer == true)
        {
            zombie.ChangeState(new ZombieChasingState());
        }
        if (zombie.canSeePlayer == false)
        {
            zombie.ChangeState(new ZombiePatrolState());
        }
    }

    public void ExitState (ZomManager zombie)
    {
    }

    private IEnumerator WaitBeforeIdle(ZomManager zombie)
    {
        yield return new WaitForSeconds(4f);
        zombie.isSpawning = false; // cho pheo doi trang thai sau khi spawn xong
        
        // Kiem tra xem Player co trong vung hay khong
        Collider[] playersInZone = Physics.OverlapSphere(zombie.transform.position, zombie.detectRange, LayerMask.GetMask("Player"));

        if (playersInZone.Length > 0)
        {
            zombie.target = playersInZone[0].transform; // Lay Player gan nhat
            zombie.canSeePlayer = true;
            zombie.ChangeState(new ZombieChasingState()); // Bat dau duoi theo
        }
        else
        {
            zombie.ChangeState(new ZombieIdleState()); // dung yen neu khong co player
        }
    }
}
