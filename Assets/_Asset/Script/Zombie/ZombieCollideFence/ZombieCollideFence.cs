using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollideFence : MonoBehaviour
{
    //public ZomManager zombieManager;
    public HealthZombie healthZombie;
    public bool isDeadElectro = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FenceLaser"))
        {
            isDeadElectro = true;
            healthZombie.Health = 0;
            healthZombie.Die();
        }
    }
}
