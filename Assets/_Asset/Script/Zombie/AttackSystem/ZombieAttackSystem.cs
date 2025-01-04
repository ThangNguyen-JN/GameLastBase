using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackSystem : MonoBehaviour
{
    public Animator anim;
    [SerializeField]public int damage;
    

    private Transform target;
    private bool isAttacking;

    public event Action<int> onDamageDealt;

    private void Start()
    {
        isAttacking = false;
    }

    public void StartAttacking(Transform player)
    {
        target = player;
        isAttacking = true;
        //anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", true);
    }

    public void StopAttacking()
    {
        target = null;
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    public void DealDamage()
    {
        if(target != null && isAttacking == true)
        {
            var healthPlayer = target.GetComponent <HealthPlayer>();
            if (healthPlayer != null)
            {
                healthPlayer.TakeDamage(damage);
                Debug.Log("Zombie Dealt Damage");
                onDamageDealt?.Invoke(damage);
            }
        }
    }
    public Transform GetCurrentTarget()
    {
        return target;
    }
}
