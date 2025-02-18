using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie : HealthManager
{
    public event Action<bool> onDead;
    public GameObject zombie;
    public DropItem dropItem;

    public ZomManager zomManager;

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
        Debug.Log("Zombie da chet! Goi su kien onDead.");
        onDead?.Invoke(true);

        Destroy(gameObject);

        if (dropItem != null)
        {
            dropItem.DropItems(transform.position);
        }

        // chuyen sang trang trai die
        if (zomManager != null)
        {
            zomManager.ChangeState(new ZombieDieState());
        }
    }

   
}
