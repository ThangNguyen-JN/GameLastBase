using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField] protected DamageHandler damageHandler;
    [SerializeField] protected FireRateHandler fireRateHandler;
    [SerializeField] protected FireRangeHandler fireRangeHandler;

    public LayerMask targetLayer;
    // Start is called before the first frame update

    protected virtual void Start()
    {

    }    
    protected bool CanShoot()
    {
        return fireRateHandler.CanShoot();
    }

    protected void ResetFireTime()
    {
        fireRateHandler.ResetFireTime();
    }

    protected bool IsTargetInRange (Transform target)
    {
        return fireRangeHandler.IsTargetInRange(target);
    }

    protected void DealDamage(GameObject target)
    {
        damageHandler.DealDamage(target);
    }

    public abstract void Shoot();
}
