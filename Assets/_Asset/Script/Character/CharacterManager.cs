using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
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
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 300;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

    public void Die()
    {

    }
}
