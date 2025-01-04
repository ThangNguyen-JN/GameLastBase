using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : HealthManager
{
    // Start is called before the first frame update
    void Start()
    {
        LoadMaxHealthData();
        Health = MaxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UpdateMaxHealth(15);
        }
    }

    public override void TakeDamage(int damage)
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
        Debug.Log($"Heal + {heal}");
        
    }

    public void UpdateMaxHealth(int health)
    {
        if (health > 0)
        {
            MaxHealth += health;
            Health = MaxHealth;
            SaveMaxHealthData();
        }
    }


    public override void Die()
    {

    }

    
    public void SaveMaxHealthData()
    {
        PlayerPrefs.SetInt("MaxHealth", MaxHealth);
        PlayerPrefs.Save();
    }

    
    public void LoadMaxHealthData()
    {
        MaxHealth = PlayerPrefs.GetInt("MaxHealth", 15);
    }

    public void OnApplicationQuit()
    {
        SaveMaxHealthData();
    }
}
