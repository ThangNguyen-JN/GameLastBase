using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeLevelDatabase turretDatabase;
    private int currentLevel = 0;
    public SphereCollider areaDetectTrigger;
    public GameObject currentTurret;
    public List<GameObject> turretsInScene;
    public UITurretUpgradeManager uiTurretUpgrade;
    //public Transform turretSpawnPoint;
    public Transform turretParent;
    private string savePath;
    
    //test id Zone
    public string zoneID;
    private string saveKey;

    public void Initialize(string newZoneID)
    {
        zoneID = newZoneID;
        saveKey = "TurretUpgrade_" + zoneID;
        savePath = Application.persistentDataPath + "/turret_" + zoneID + "_save.json";
        LoadTurretData();

        
    }

    public void Start()
    {
        //savePath = Application.persistentDataPath + "/turret_save.json";
        //LoadTurretData();
        if (string.IsNullOrEmpty(zoneID))
        {
            Debug.LogError("ZoneID is not set for " + gameObject.name);
            return;
        }

        saveKey = "TurretUpgrade_" + zoneID;
        savePath = Application.persistentDataPath + "/turret_" + zoneID + "_save.json";
        LoadTurretData();
    }

    public void UpgradeComplete()
    {
        if (currentLevel >= turretDatabase.turretUpLevel.Count)
        {
            Debug.Log("Max Level Reached!");
            return;
        }
        // lay cap do tiep theo
        TurretUpgradeLevel nextLevel = turretDatabase.turretUpLevel[currentLevel];

        //kich hoat turret moi
        currentLevel++;
        ActivateTurret(currentLevel - 1, currentLevel);//+ 1);
        areaDetectTrigger.radius += 1;
        SaveTurretData();

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

    public TurretUpgradeLevel GetNextLevel()
    {
        if (currentLevel < turretDatabase.turretUpLevel.Count)
        {
            return turretDatabase.turretUpLevel[currentLevel];
        }
        return null; // Nếu đã đạt cấp tối đa
    }

    [ContextMenu("SaveUpgradeData")]
    public void SaveTurretData()
    {
        if (currentLevel >= turretDatabase.turretUpLevel.Count) return;

        TurretUpgradeSaveData saveData = new TurretUpgradeSaveData
        {
            turretLevel = turretDatabase.turretUpLevel[currentLevel].levelTurret,
            requiredResources = turretDatabase.turretUpLevel[currentLevel].requiredResources
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

    public void LoadTurretData()
    {
        string json = "";

        if (File.Exists(savePath))
        {
            json = File.ReadAllText(savePath);
        }
        else if (PlayerPrefs.HasKey(saveKey))
        {
            json = PlayerPrefs.GetString(saveKey);
            Debug.Log($"Loaded turret data from PlayerPrefs for {zoneID}");
        }
        //else if (PlayerPrefs.HasKey("TurretUpgradeZone1"))
        //{
        //    json = PlayerPrefs.GetString("TurretUpgradeZone1");
        //    Debug.Log("Loaded turret data from PlayerPrefs.");
        //}
       

        if (!string.IsNullOrEmpty(json))
        {
            TurretUpgradeSaveData loadedData = JsonUtility.FromJson<TurretUpgradeSaveData>(json);

            int loadedLevel = turretDatabase.turretUpLevel.FindIndex(level => level.levelTurret == loadedData.turretLevel);
            if (loadedLevel < 0) loadedLevel = 0;

            currentLevel = loadedLevel;
            turretDatabase.turretUpLevel[currentLevel].requiredResources = loadedData.requiredResources;

            Debug.Log($"Loaded turret level {currentLevel} with saved resources.");

            // Bật đúng turret khi load
            for (int i = 0; i < turretsInScene.Count; i++)
            {
                turretsInScene[i].SetActive(i == currentLevel);
            }

            if (uiTurretUpgrade != null)
            {
                uiTurretUpgrade.UpdateUI();
            }
            
        }
        
    }

    public void OnApplicationQuit()
    {
        SaveTurretData();
    }

    [System.Serializable]
    public class TurretUpgradeSaveData
    {
        public string turretLevel;
        public List<ResourceUpgradeTurret> requiredResources;
    }

    [ContextMenu("Reset Turret Data")]
    public void ResetTurretData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log($"Deleted save file: {savePath}");
        }

        if (PlayerPrefs.HasKey(saveKey))
        {
            PlayerPrefs.DeleteKey(saveKey);
            PlayerPrefs.Save();
            Debug.Log($"Deleted PlayerPrefs key: {saveKey}");
        }

        // Đặt lại level về mặc định
        currentLevel = 0;
        for (int i = 0; i < turretsInScene.Count; i++)
        {
            turretsInScene[i].SetActive(i == currentLevel);
        }

        if (uiTurretUpgrade != null)
        {
            uiTurretUpgrade.UpdateUI();
        }

        Debug.Log($"Reset turret data for {zoneID}");
    }
}




