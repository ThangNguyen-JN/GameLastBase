using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallShoot : MonoBehaviour
{
    public GunPlayer gunPlayer;

    public void PlayerCallShoot()
    {
        gunPlayer.Shoot();
    }    
}
