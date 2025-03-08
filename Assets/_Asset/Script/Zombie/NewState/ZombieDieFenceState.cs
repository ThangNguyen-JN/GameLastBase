using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDieFenceState : IZombieState
{
    public void EnterState(ZomManager zombie)
    {
        Debug.Log("Zombie is die by electro");
        zombie.movement.ZombieDie();


        if (zombie.anim != null)
        {
            zombie.anim.SetBool("isDeadElectro", true); // kick hoat animation 
        }

        zombie.StartCoroutine(DieCoroutine(zombie));
    }

    public void UpdateState(ZomManager zombie) { }

    public void ExitState(ZomManager zombie) { }

    private IEnumerator DieCoroutine(ZomManager zombie)
    {
        yield return new WaitForSeconds(2f); // cho animation hoan thanh
        GameObject.Destroy(zombie.gameObject); // huy zombie
    }
}
