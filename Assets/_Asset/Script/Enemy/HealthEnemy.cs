using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    private int enemyHealth;

    public int EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = Mathf.Clamp(value, 0, 100); }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <=0)
        {
            Die();
        }
    }

    public void Die()
    {

    }
}
