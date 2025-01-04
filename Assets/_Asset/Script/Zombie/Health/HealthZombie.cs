using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie : HealthManager
{
    public event Action<bool> onDead;
    public GameObject zombie;

    public bool isDead = false;
    private void Start()
    {
        Health = MaxHealth;
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <=0) 
        {
            Die();
        }
    }

    public override void Die()
    {
        isDead = true;
        onDead?.Invoke(isDead);
        Destroy(gameObject);
        Destroy(zombie, 4f);
    }
}
