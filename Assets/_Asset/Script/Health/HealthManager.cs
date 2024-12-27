using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged;

    private int currentHealth;
    [SerializeField]private int maxHealth;
    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (value > 0)
            {
                maxHealth = value;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                OnHealthChanged?.Invoke(currentHealth, maxHealth);
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (currentHealth <=0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}
