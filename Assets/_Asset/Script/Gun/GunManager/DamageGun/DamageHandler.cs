using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Clamp(value, 2, 150);}
    }

    private void Start()
    {
        LoadDamage();
    }


    public void DealDamage(GameObject target)
    {
        if (target == null) return;
        
        var healthZombie = target.GetComponent<HealthZombie>();
        if (healthZombie != null) 
        {
            healthZombie.TakeDamage(Damage);
            
        }
        
    }     
    
    public void UpgradeDamageGun(int value)
    {
        Damage += value;
        SaveDamage();
    }

    public void SaveDamage()
    {
        PlayerPrefs.SetInt("DamageGun", damage);
        PlayerPrefs.Save();
    }

    public void LoadDamage()
    {
        damage = PlayerPrefs.GetInt("DamageGun", 2);
    }

    public void OnApplicationQuit()
    {
        SaveDamage();
    }
}

