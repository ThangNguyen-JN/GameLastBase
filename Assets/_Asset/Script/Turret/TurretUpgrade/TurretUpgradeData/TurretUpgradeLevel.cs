using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TurretUpgradeLevel
{
    public string levelTurret;
    public List<ResourceUpgradeTurret> requiredResources;
}

[Serializable]
public class TurretUpgradeSaveData
{
    public string turretLevel;
    public List<ResourceUpgradeSaveData> requiredResources;
    public float areaRadius;
}

[Serializable]
public class ResourceUpgradeTurret
{
    public string nameResource;
    public Sprite imageResource;
    public int quantilyResource;
}

[System.Serializable]
public class ResourceUpgradeSaveData
{
    public string nameResource;
    public int quantilyResource;
}


