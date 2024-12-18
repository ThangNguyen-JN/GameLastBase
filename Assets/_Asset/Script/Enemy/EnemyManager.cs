using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private int enemyHealth;

    [SerializeField]private int maxHealth;
    public int EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = Mathf.Clamp(value, 0, 200); }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamage(int damage)
    {
        EnemyHealth -= damage;
        Debug.Log($"Enemy Health: {EnemyHealth}");
        if (EnemyHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
