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

        List<ResourceUpgradeSaveData> saveResources = new List<ResourceUpgradeSaveData>();
        foreach (var resource in turretDatabase.turretUpLevel[currentLevel].requiredResources)
        {
            saveResources.Add(new ResourceUpgradeSaveData { nameResource = resource.nameResource, quantilyResource = resource.quantilyResource });
        }

        TurretUpgradeSaveData saveData = new TurretUpgradeSaveData
        {
            turretLevel = turretDatabase.turretUpLevel[currentLevel].levelTurret,
            requiredResources = saveResources
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
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

        if (!string.IsNullOrEmpty(json))
        {
            TurretUpgradeSaveData loadedData = JsonUtility.FromJson<TurretUpgradeSaveData>(json);
            int loadedLevel = turretDatabase.turretUpLevel.FindIndex(level => level.levelTurret == loadedData.turretLevel);
            if (loadedLevel < 0) loadedLevel = 0;

            currentLevel = loadedLevel;
            Debug.Log($"Loaded turret level {currentLevel}");

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

}
