using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TurretUpgradeLevel
{
    public string levelTurret;
    public List<ResourceUpgradeTurret> requiredResources;
    //public int damageIncrease;

}


[Serializable]
public class ResourceUpgradeTurret
{
    public string nameResource;
    public Sprite imageResource;
    public int quantilyResource;
}
