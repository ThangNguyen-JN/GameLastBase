using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeLeverDatabase turretDatabase;
    public ResourceDatabase resourceDatabase;
    private int currentLevel = 0;
    public SphereCollider areaDetectTrigger;
    public GameObject currentTurret;
    public List<GameObject> turretsInScene;
    //public Transform turretSpawnPoint;
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
            ActivateTurret(currentLevel, currentLevel + 1);
            areaDetectTrigger.radius+=1;
            //UpgradeTurretDamage(nextLevel.damageIncrease);
            currentLevel++; // Tăng cấp
            return true;
        }

        Debug.Log("Not enough resources to upgrade!");
        return false;
    }

    private void ActivateTurret(int oldLevel, int newLevel)
    {
        // Tắt turret cũ
        if (oldLevel < turretsInScene.Count && turretsInScene[oldLevel] != null)
        {
            turretsInScene[oldLevel].SetActive(false);
        }

        // Bật turret mới
        if (newLevel < turretsInScene.Count && turretsInScene[newLevel] != null)
        {
            turretsInScene[newLevel].SetActive(true);
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
