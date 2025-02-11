using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTriggerUpgrade : MonoBehaviour
{
    public StorageUpgradeManage storageUpManager;
    public StorageUpgradeUI storageUpUi;
    public ResourceDatabase resourceData;

    public bool isUpgrading = false;
    private bool isPlayerInside = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!isUpgrading && other.CompareTag("PlayerUpgrade"))
        {
            isPlayerInside = true;
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
        StorageUpgradeLevel nextLevel = storageUpManager.GetNextLevel();
        if (nextLevel == null)
        {
            Debug.Log("Turret max lever");
            yield break;
        }

        while (isPlayerInside == true)
        {
            bool upgrading = false;
            foreach (var resource in nextLevel.resourceUpgradeStorages)
            {
                if (resource.quantityResource > 0)
                {
                    int availableAmount = resourceData.GetResource(resource.nameResource).amount;
                    if (availableAmount > 0)
                    {
                        int subtractAmount = Mathf.Min(availableAmount, 1); // Trừ từng chút một
                        resourceData.SubtractResource(resource.nameResource, subtractAmount);
                        resource.quantityResource -= subtractAmount;
                        upgrading = true; // Còn tài nguyên để trừ thì tiếp tục lặp
                    }
                }

            }
            storageUpUi.UpdateUIStorage();
            yield return new WaitForSeconds(0.3f);
            if (!upgrading) break;
        }

        if (AllResourcesDepleted(nextLevel))
        {
            storageUpManager.UpgradeStorageComplete();
        }

        resourceData.SaveResource();
        storageUpUi.UpdateUIStorage();
        yield return new WaitForSeconds(2f);
        isUpgrading = false;
    }

    private bool AllResourcesDepleted(StorageUpgradeLevel nextLevel)
    {
        foreach (var resource in nextLevel.resourceUpgradeStorages)
        {
            if (resource.quantityResource > 0)
                return false;
        }
        return true;
    }
}
