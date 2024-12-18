using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField]private int damage;
    

    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Clamp(value, 1, 150);}
    }

    public void DealDamage(GameObject target)
    {
        var enemyManager = target.GetComponent<EnemyManager>();
        if (enemyManager != null) 
        { 
            enemyManager.TakeDamage(damage);
        }
        
    }       
}

