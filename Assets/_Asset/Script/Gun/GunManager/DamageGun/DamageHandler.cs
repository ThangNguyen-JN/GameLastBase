using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private int damage;
    

    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Clamp(value, 1, 150);}
    }

    public void DealDamage(GameObject target)
    {
        var healthEnemy = target.GetComponent<HealthEnemy>();
        if (healthEnemy != null) 
        { 
            healthEnemy.TakeDamage(damage);
        }
        
    }       
}

