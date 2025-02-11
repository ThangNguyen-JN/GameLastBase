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

    private string savePath;
    public string storageID;
    private string saveKey;

    public void Initialize(string newZoneID)
    {
        if (string.IsNullOrEmpty(newZoneID))
        {
            Debug.LogError("ZoneID is null or empty! Cannot initialize.");
            return;
        }

        storageID = newZoneID;
        saveKey = "StorageUpgrade_" + storageID;
        savePath = Application.persistentDataPath + "/storage_" + storageID + "_save.json";
        LoadStorageData();
    }
    public void Start()
    {
        //savePath = Application.persistentDataPath + "/turret_save.json";
        //LoadTurretData();
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
        if (currentLevel >= storageData.storageUpLevel.Count) return;

        StorageUpgradeSaveData saveData = new StorageUpgradeSaveData
        {
            storageLevel = storageData.storageUpLevel[currentLevel].levelStorage,
            resourceUpgradeStorages = storageData.storageUpLevel[currentLevel].resourceUpgradeStorages
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);

        // Test
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
        //PlayerPrefs.SetString("TurretUpgradeZone1", json);
        //PlayerPrefs.Save();
        Debug.Log($"Saved turret data at {savePath}");
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
        //else if (PlayerPrefs.HasKey("TurretUpgradeZone1"))
        //{
        //    json = PlayerPrefs.GetString("TurretUpgradeZone1");
        //    Debug.Log("Loaded turret data from PlayerPrefs.");
        //}


        if (!string.IsNullOrEmpty(json))
        {
            StorageUpgradeSaveData loadedData = JsonUtility.FromJson<StorageUpgradeSaveData>(json);

            int loadedLevel = storageData.storageUpLevel.FindIndex(level => level.levelStorage == loadedData.storageLevel);
            if (loadedLevel < 0) loadedLevel = 0;

            currentLevel = loadedLevel;
            storageData.storageUpLevel[currentLevel].resourceUpgradeStorages = loadedData.resourceUpgradeStorages;

            Debug.Log($"Loaded turret level {currentLevel} with saved resources.");

            // Bật đúng turret khi load
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

    [System.Serializable]
    public class StorageUpgradeSaveData
    {
        public string storageLevel;
        public List<ResourceUpgradeStorage> resourceUpgradeStorages;
    }

}
