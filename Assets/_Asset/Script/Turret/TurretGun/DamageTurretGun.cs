using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTurretGun : MonoBehaviour
{
    [SerializeField] private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Clamp(value, 2, 150); }
    }

    private void Start()
    {
    }

    public void UpgradeDamage(int value)
    {
        Damage += value;
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
}
