using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class StorageUpgradeManage : MonoBehaviour
{
    public StorageUpgradeLevelDatabase storageData;
    public int currentLevel = 0;
    public GameObject currentStorage;
    public List<GameObject> storageInScene;
    public StorageUpgradeUI storageUpgradeUI;
    public StorageQuantity storageQuantity;

    private string savePath;
    public string storageID;
    private string saveKey;


    public static StorageUpgradeManage Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
      
        Debug.Log("📢 Awake() called for " + gameObject.name);

        if (string.IsNullOrEmpty(storageID))
        {
            Debug.LogError("ZoneID is not set for " + gameObject.name);
            return;
        }

        saveKey = "StorageUpgrade_" + storageID;
        savePath = Application.persistentDataPath + "/storage_" + storageID + "_save.json";
        LoadStorageData();
    }

    public void UpgradeStorageComplete()
    {
        if ( currentLevel >= storageData.storageUpLevel.Count)
        {
            return;
        }

        StorageUpgradeLevel nextLevel = storageData.storageUpLevel[currentLevel];

        currentLevel++;
        ActivateStorage(currentLevel - 1, currentLevel);
        storageQuantity.UpgradeMaxResource(5);
        SaveStorageData();
    }

    private void ActivateStorage(int oldLevel, int newLevel)
    {
        // Tat storage cu
        if (oldLevel < storageInScene.Count && storageInScene[oldLevel] != null)
        {
            storageInScene[oldLevel].SetActive(false);
        }

        // Bat storage moi
        if (newLevel < storageInScene.Count && storageInScene[newLevel] != null)
        {
            storageInScene[newLevel].SetActive(true);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }    

    public StorageUpgradeLevel GetNextLevel()
    {
        if (currentLevel < storageData.storageUpLevel.Count)
        {
            return storageData.storageUpLevel[currentLevel];
        }
        return null;
    }

    [ContextMenu("SaveUpgradeData")]
    public void SaveStorageData()
    {
        if (string.IsNullOrEmpty(savePath))
        {
            return;
        }

        if (currentLevel >= storageData.storageUpLevel.Count) return;

        List<ResourceUpgradeStorageSaveData> saveResources = new List<ResourceUpgradeStorageSaveData>();
        foreach (var resource in storageData.storageUpLevel[currentLevel].resourceUpgradeStorages)
        {
            saveResources.Add(new ResourceUpgradeStorageSaveData
            {
                nameResource = resource.nameResource,
                quantityResource = resource.quantityResource
            });
        }

        StorageUpgradeSaveData saveData = new StorageUpgradeSaveData
        {
            storageLevel = storageData.storageUpLevel[currentLevel].levelStorage,
            resourceUpgradeStorages = saveResources,
            currentResource = storageQuantity.CurrentResource,
            maxResource = storageQuantity.maxResource
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadStorageData()
    {
        string json = "";

        if (File.Exists(savePath))
        {
            json = File.ReadAllText(savePath);
        }
        else if (PlayerPrefs.HasKey(saveKey))
        {
            json = PlayerPrefs.GetString(saveKey);
        }

        if (!string.IsNullOrEmpty(json))
        {
            StorageUpgradeSaveData loadedData = JsonUtility.FromJson<StorageUpgradeSaveData>(json);
            int loadedLevel = storageData.storageUpLevel.FindIndex(level => level.levelStorage == loadedData.storageLevel);
            if (loadedLevel < 0) loadedLevel = 0;

            currentLevel = loadedLevel;

            // Chuyển đổi từ SaveData về runtime data
            storageData.storageUpLevel[currentLevel].resourceUpgradeStorages.Clear();
            foreach (var resource in loadedData.resourceUpgradeStorages)
            {
                storageData.storageUpLevel[currentLevel].resourceUpgradeStorages.Add(new ResourceUpgradeStorage
                {
                    nameResource = resource.nameResource,
                    quantityResource = resource.quantityResource,
                    //imageResource = null // Không lưu Sprite
                });
            }

            storageQuantity.CurrentResource = loadedData.currentResource;
            storageQuantity.maxResource = loadedData.maxResource;

            for (int i = 0; i < storageInScene.Count; i++)
            {
                storageInScene[i].SetActive(i == currentLevel);
            }

            if (storageUpgradeUI != null)
            {
                storageUpgradeUI.UpdateUIStorage();
            }
        }

    }

}
