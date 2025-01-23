using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretUpgradeLeverData", menuName = "Turret Upgrade/Lever Data")]
public class TurretUpgradeLeverDatabase : ScriptableObject
{
    public List<TurretUpgradeLever> turretUpLever = new List<TurretUpgradeLever>();

}  
