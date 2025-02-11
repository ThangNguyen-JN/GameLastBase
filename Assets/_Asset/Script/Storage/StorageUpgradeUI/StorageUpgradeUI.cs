using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageUpgradeUI : MonoBehaviour
{
    public StorageUpgradeManage storageUpManager;
    public Transform resourceListContainer;
    public GameObject resourceItemPrefab;

    public void Awake()
    {
        UpdateUIStorage();
    }

    public void UpdateUIStorage()
    {
        foreach (Transform child in resourceListContainer)
        {
            Destroy(child.gameObject);
        }

        int currentLevel = storageUpManager.GetCurrentLevel();
        StorageUpgradeLevel currentLevelData = storageUpManager.GetNextLevel();
        
        if (currentLevelData == null)
        {
            Debug.LogWarning("Don't Find Level Storage!");
            return;
        }

        foreach (var resource in currentLevelData.resourceUpgradeStorages)
        {
            GameObject resourceItem = Instantiate(resourceItemPrefab, resourceListContainer);

            // Tim cac thanh phan UI trong Prefab
            Image iconImage = resourceItem.transform.Find("ResourceImage").GetComponent<Image>();
            Text quantityText = resourceItem.transform.Find("ResourceText").GetComponent<Text>();

            // Cap nhat du lieu
            iconImage.sprite = resource.imgaeResource;
            quantityText.text = resource.quantityResource.ToString();
        }


    }
}
