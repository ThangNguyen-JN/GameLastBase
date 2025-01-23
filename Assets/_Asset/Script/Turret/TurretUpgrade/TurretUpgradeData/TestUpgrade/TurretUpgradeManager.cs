using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeLeverDatabase turretDatabase;
    public ResourceDatabase resourceDatabase;
    public DamageTurretGun damageTurret;
    private int currentLevel = 0;

    public GameObject currentTurret;
    public Transform turretSpawnPoint;
    public Transform turretParent;


    public bool TryUpgrade()
    {
        // Kiểm tra nếu đã đạt cấp tối đa
        if (currentLevel >= turretDatabase.turretUpLever.Count)
        {
            Debug.Log("Max Level Reached!");
            return false;
        }

        // Lấy cấp độ tiếp theo
        TurretUpgradeLever nextLevel = turretDatabase.turretUpLever[currentLevel];

        // Kiểm tra tài nguyên
        if (resourceDatabase.HasEnoughResources(nextLevel.requiredResources))
        {
            resourceDatabase.DeductResources(nextLevel.requiredResources); // Trừ tài nguyên
            SpawnTurret(nextLevel.turretPrefab);
            UpgradeTurretDamage(nextLevel.damageIncrease);
            currentLevel++; // Tăng cấp
            return true;
        }

        Debug.Log("Not enough resources to upgrade!");
        return false;
    }

    private void SpawnTurret(GameObject turretPrefab)
    {
        if (currentTurret != null)
        {
            Destroy(currentTurret); // Hủy turret hiện tại
        }

        if (turretPrefab != null)
        {
            currentTurret = Instantiate(turretPrefab, turretSpawnPoint.position, turretSpawnPoint.rotation, turretParent);
        }
    }

    private void UpgradeTurretDamage(int damageIncrease)
    {
        if (currentTurret == null) return;

        
        if (damageTurret != null)
        {
            damageTurret.UpgradeDamage(damageIncrease);
            Debug.Log($"Increased turret damage by {damageIncrease}, current damage: {damageTurret.Damage}");
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public TurretUpgradeLever GetNextLevel()
    {
        if (currentLevel < turretDatabase.turretUpLever.Count)
        {
            return turretDatabase.turretUpLever[currentLevel];
        }
        return null; // Nếu đã đạt cấp tối đa
    }
}
