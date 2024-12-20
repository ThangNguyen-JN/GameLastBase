using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged;

    private int currentHealth;
    private int maxHealth;
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
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 15;
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateMaxHealth(15);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(3);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Heal(2);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) 
        { 
            Die();
        }
    }

    public void Heal(int heal)
    {
        Health += heal;
        
    }

    public void UpdateMaxHealth(int health)
    {
        if (health > 0)
        {
            MaxHealth += health;
            Health = MaxHealth;
        }
    }


    public void Die()
    {

    }
}
