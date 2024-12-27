using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie : HealthManager
{
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

    }
}
