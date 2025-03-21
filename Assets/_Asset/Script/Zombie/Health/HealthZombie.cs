using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthZombie : HealthManager
{
    public event Action<bool> onDead;
    public GameObject zombie;
    public DropItem dropItem;

    public ZomManager zomManager;
    public ZombieCollideFence zombieCollideFence;

    public bool isDead = false;
    private void Start()
    {
        Health = MaxHealth;
    }

    public override void TakeDamage(int damage)
    {
        if (isDead) return;
        Health -= damage;
        if (Health <=0) 
        {
            Die();
        }
    }

    public override void Die()
    {
        if (isDead) return; 

        isDead = true;
        onDead?.Invoke(true);

        if (dropItem != null)
        {
            dropItem.DropItems(transform.position);
        }

        // chuyen sang trang trai die

        if (zomManager != null)
        {
            //zomManager.ChangeState(new ZombieDieState());
            if (zombieCollideFence != null && zombieCollideFence.isDeadElectro == true)
            {
                zomManager.ChangeState(new ZombieDieFenceState());
            }
            else
            {
                zomManager.ChangeState(new ZombieDieState());
            }
        }

        NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();
        if (agent != null) 
        {
            agent.enabled = false;
        }

        StopAllCoroutines();
        Destroy(gameObject);
    }

   
}
