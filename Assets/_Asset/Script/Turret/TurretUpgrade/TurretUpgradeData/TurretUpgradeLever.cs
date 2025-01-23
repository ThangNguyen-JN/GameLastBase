using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TurretUpgradeLever
{
    public string leverTurret;
    public List<ResourceUpgradeTurret> requiredResources;
    public GameObject turretPrefab;
    public int damageIncrease;

}


[Serializable]
public class ResourceUpgradeTurret
{
    public string nameResource;
    public Sprite imageResource;
    public int quantilyResource;
}
