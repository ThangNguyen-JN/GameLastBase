using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField] protected DamageHandler damageHandler;
    [SerializeField] protected FireRateHandler fireRateHandler;

    public LayerMask targetLayer;
    // Start is called before the first frame update

    protected virtual void Start()
    {

    }    
    public bool CanShoot()
    {
        return fireRateHandler != null && fireRateHandler.CanShoot();
    }

    public void ResetFireTime()
    {
        if (fireRateHandler != null)
        {
            fireRateHandler.ResetFireTime();
        }
    }  

    protected void DealDamage(GameObject target)
    {
        if (damageHandler != null && target != null)
        {
            damageHandler.DealDamage(target);
        }
    }

    public abstract void Shoot();
}
