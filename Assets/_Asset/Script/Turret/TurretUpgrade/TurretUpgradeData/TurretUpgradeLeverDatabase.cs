using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretUpgradeLeverData", menuName = "Turret Upgrade/Lever Data")]
public class TurretUpgradeLeverDatabase : ScriptableObject
{
    public List<TurretUpgradeLever> turretUpLever = new List<TurretUpgradeLever>();

    public event Action<TurretUpgradeLever> OnResourceUpgradeChanged;

    public TurretUpgradeLever GetResourceUpgradeTurret (string nameResource)
    {
        return turretUpLever.Find(r => r.nameResource == nameResource);
    }

    public void SubtractResourceUpTurret(string nameResource, int amount)
    {
        TurretUpgradeLever turretUpLever = GetResourceUpgradeTurret(nameResource);
        if (turretUpLever != null)
        {
            turretUpLever.SubtractAmountResource(amount);
            OnResourceUpgradeChanged?.Invoke(turretUpLever);
            
        }
    }
}
