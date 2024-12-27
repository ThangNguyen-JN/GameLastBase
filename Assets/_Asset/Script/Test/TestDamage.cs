using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public HealthPlayer healthPlayer;
    public HealthZombie healthZombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            healthPlayer.TakeDamage(2);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            healthZombie.TakeDamage(2);
        }
    }
}
