using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretUpgradeLeverData", menuName = "Turret Upgrade/Lever Data")]
public class TurretUpgradeLevelDatabase : ScriptableObject
{
    public List<TurretUpgradeLevel> turretUpLevel = new List<TurretUpgradeLevel>();

}  
