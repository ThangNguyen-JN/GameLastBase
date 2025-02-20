using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StorageUpgradeLevel
{
    public string levelStorage;
    public List<ResourceUpgradeStorage> resourceUpgradeStorages;
}

[Serializable]
public class ResourceUpgradeStorage
{
    public string nameResource;
    public Sprite imageResource;
    public int quantityResource;
}

[Serializable]
public class ResourceUpgradeStorageSaveData
{
    public string nameResource;
    public int quantityResource;
}

[System.Serializable]
public class StorageUpgradeSaveData
{
    public string storageLevel;
    public List<ResourceUpgradeStorageSaveData> resourceUpgradeStorages;
    public int currentResource;
    public int maxResource;
}

