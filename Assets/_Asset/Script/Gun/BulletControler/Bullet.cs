using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    private Vector3 direction;

    public DamageHandler damageHandler;


    public void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }

    public void Initialize (Vector3 direction)
    {
        this.direction = direction;
        
    }    

    public void Initialize (Vector3 shootDirection, int bulletDamage)
    {
        direction = shootDirection.normalized;

        //Gan DamageHandler vao dan neu chua co
        if (damageHandler == null)
        {
            damageHandler = gameObject.AddComponent<DamageHandler>();
        }
        damageHandler.Damage = bulletDamage;
    }    

    // Update is called once per frame
    void Update()
    {
       transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Zombie"))
        {
            damageHandler.DealDamage(other.gameObject);
            Destroy(gameObject);
            
        }
    }

}
