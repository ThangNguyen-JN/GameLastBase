using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public ZombieMovementWithBoxTrigger zombieMovement;
    public HealthZombie healthZombie;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        zombieMovement.onWaitingStateChanged += UpdateAnimation;
        UpdateAnimation(zombieMovement.IsWaiting);

        healthZombie.onDead += UpdateAnimDie;
        UpdateAnimDie(healthZombie.isDead);

    }

    public void UpdateAnimation(bool isWaiting)
    {
        if(isWaiting == true)
        {
            anim.SetBool("isMoving", false);
        }

        if(isWaiting == false)
        {
            anim.SetBool("isMoving", true);
        }
    }

    public void UpdateAnimDie(bool isDead)
    {
        if (isDead == true)
        {
            anim.SetBool("isDead", true);
        }
    }

    private void OnDestroy()
    {
        zombieMovement.onWaitingStateChanged -= UpdateAnimation;
        healthZombie.onDead -= UpdateAnimDie;
    }

}
