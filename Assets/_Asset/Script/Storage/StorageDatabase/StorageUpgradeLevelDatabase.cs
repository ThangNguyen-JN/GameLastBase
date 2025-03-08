using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageUpgradeLevelData", menuName = "Storage Upgrade/Level Data")]
public class StorageUpgradeLevelDatabase : ScriptableObject
{
    public List<StorageUpgradeLevel> storageUpLevel = new List<StorageUpgradeLevel>();
}

