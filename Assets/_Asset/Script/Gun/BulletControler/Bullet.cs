using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 1f;

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
        direction = new Vector3(shootDirection.x, 0, shootDirection.z).normalized;

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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            damageHandler.DealDamage(other.gameObject);
            Destroy(gameObject);
        }    
    }    
}
