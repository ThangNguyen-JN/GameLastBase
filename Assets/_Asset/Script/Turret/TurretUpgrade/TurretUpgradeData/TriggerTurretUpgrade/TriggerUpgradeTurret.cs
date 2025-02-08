using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpgradeTurret : MonoBehaviour
{

    public TurretUpgradeManager turretUpgradeManager;
    public UITurretUpgradeManager uiTurretUpgrade;
    public ResourceDatabase resourceDatabase;

    public bool isUpgrading = false;
    private bool isPlayerInside = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!isUpgrading && other.CompareTag("PlayerUpgrade"))
        {
            isPlayerInside = true;
            Debug.Log("Trigger Turret Upgrade");
            StartCoroutine(MinusResourcesAndUpgrade());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
        {
            isPlayerInside = false;
        }
    }

    public IEnumerator MinusResourcesAndUpgrade()
    {
        yield return new WaitForSeconds(1f);
        isUpgrading = true;
        TurretUpgradeLevel nextLevel = turretUpgradeManager.GetNextLevel();
        if (nextLevel == null)
        {
            Debug.Log("Turret max lever");
            yield break;
        }

        while (isPlayerInside == true)
        {
            bool upgrading = false;
            foreach (var resource in nextLevel.requiredResources)
            {
                if (resource.quantilyResource > 0)
                {
                    int availableAmount = resourceDatabase.GetResource(resource.nameResource).amount;
                    if (availableAmount > 0)
                    {
                        int subtractAmount = Mathf.Min(availableAmount, 1); // Trừ từng chút một
                        resourceDatabase.SubtractResource(resource.nameResource, subtractAmount);
                        resource.quantilyResource -= subtractAmount;
                        upgrading = true; // Còn tài nguyên để trừ thì tiếp tục lặp
                    }
                }
                
            }
            uiTurretUpgrade.UpdateUI();
            yield return new WaitForSeconds(0.3f);
            if (!upgrading) break;
        }

        if (AllResourcesDepleted(nextLevel))
        {
            turretUpgradeManager.UpgradeComplete();
        }

        resourceDatabase.SaveResource();
        uiTurretUpgrade.UpdateUI();
        yield return new WaitForSeconds(2f);
        isUpgrading = false;
    }
    private bool AllResourcesDepleted(TurretUpgradeLevel nextLevel)
    {
        foreach (var resource in nextLevel.requiredResources)
        {
            if (resource.quantilyResource > 0)
                return false;
        }
        return true;
    }
}
