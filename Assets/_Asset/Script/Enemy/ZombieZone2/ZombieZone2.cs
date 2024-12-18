using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZone2 : EnemyManager
{
    public override void Die()
    {
        Debug.Log("ZombieZone1 Die + drop 2 skull");
        Destroy(gameObject);
        base.Die();

    }
}
