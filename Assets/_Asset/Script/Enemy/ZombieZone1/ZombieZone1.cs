using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZone1 : EnemyManager
{
    // Start is called before the first frame update
    public override void Die()
    {
        Debug.Log("ZombieZone1 Die + drop 1 skull");
        Destroy(gameObject);
        base.Die();

    }
}
