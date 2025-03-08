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
    public StorageLevelHandler storageLevelHandler;

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
        if (storageLevelHandler != null)
        {
            storageLevelHandler.UpdateObjectState();
        }
    }

    private void ActivateStorage(int oldLevel, int newLevel)
    {
        if (oldLevel >= 0 && oldLevel < storageInScene.Count && storageInScene[oldLevel] != null)
        {
            storageInScene[oldLevel].SetActive(false);
        }

        // Kiểm tra newLevel hợp lệ trước khi bật storage mới
        if (newLevel >= 0 && newLevel < storageInScene.Count && storageInScene[newLevel] != null)
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
            //storageData.storageUpLevel[currentLevel].resourceUpgradeStorages.Clear();
            List<ResourceUpgradeStorage> tempList = new List<ResourceUpgradeStorage>();
            foreach (var resource in loadedData.resourceUpgradeStorages)
            {
                tempList.Add(new ResourceUpgradeStorage
                {
                    nameResource = resource.nameResource,
                    quantityResource = resource.quantityResource
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

    public void OnApplicationQuit()
    {
        SaveStorageData();
    }

    [ContextMenu("ResetStorageLevel")]
    public void ResetStorageLevel()
    {
        currentLevel = 0; // Đặt về level 0
        
        storageQuantity.CurrentResource = 0; // Đặt lại tài nguyên hiện tại về 0
        storageQuantity.maxResource = 20;

        foreach (var storage in storageInScene)
        {
            if (storage != null)
                storage.SetActive(false);
        }

        // Bật lại storage level 0
        if (storageInScene.Count > 0 && storageInScene[0] != null)
        {
            storageInScene[0].SetActive(true);
        }

        SaveStorageData(); // Lưu lại trạng thái sau khi reset

        if (storageUpgradeUI != null)
        {
            storageUpgradeUI.UpdateUIStorage(); // Cập nhật UI
        }

        Debug.Log("Storage level has been reset to 0!");
    }

}




