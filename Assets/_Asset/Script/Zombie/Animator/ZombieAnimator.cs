using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public ZombieMovementWithBoxTrigger zombieMovement;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        zombieMovement.onWaitingStateChanged += UpdateAnimation;
        UpdateAnimation(zombieMovement.IsWaiting);
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

    private void OnDestroy()
    {
        zombieMovement.onWaitingStateChanged -= UpdateAnimation;
    }

}
