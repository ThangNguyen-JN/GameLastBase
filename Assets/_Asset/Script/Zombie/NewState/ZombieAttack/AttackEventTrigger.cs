using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventTrigger : MonoBehaviour
{
    public ZomManager zombieManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackEvent()
    {
        zombieManager.DealDamage();
    }
}
