using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnState : IZombieState
{
    public void EnterState(ZomManager zombie)
    {
        zombie.isSpawning = true;
        zombie.movement.StopMoving();
        if (!zombie.anim.GetCurrentAnimatorStateInfo(0).IsName("isSpawn"))
        {
            zombie.anim.SetTrigger("isSpawn"); 
        }
        zombie.StartCoroutine(WaitBeforeIdle(zombie));
    }

    public void UpdateState(ZomManager zombie)
    {
       
    }

    public void ExitState(ZomManager zombie) { }

    private IEnumerator WaitBeforeIdle(ZomManager zombie)
    {
        yield return new WaitForSeconds(4f);
        zombie.isSpawning = false;
        zombie.ChangeState(new ZombieIdleState());
    }
}
