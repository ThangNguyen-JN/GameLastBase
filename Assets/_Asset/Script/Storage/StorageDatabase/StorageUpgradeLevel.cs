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
    public Sprite imgaeResource;
    public int quantityResource;
}
