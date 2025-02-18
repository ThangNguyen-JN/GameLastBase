using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "ResourceDatabase", menuName = "Resources/ResourceDatabase")]
public class ResourceDatabase : ScriptableObject
{
    //Signleton Instance
    private static ResourceDatabase instance;
    public static ResourceDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ResourceDatabase>("ResourcesSystem/ResourceData/ResourceDatabase");
                if (instance == null)
                {
                    Debug.Log("ResourceDatabase Erro");
                }
            }
            return instance;
        }
    }    

    //Danh sach cac tai nguyen trong co so du lieu
    public List<Resource> resources = new List<Resource>();

    //Su kien de thong bao khi thay doi mot tai nguyen
    public event Action<Resource> OnResourceChanged;

    private string saveFilePath;

    //tim va tra ve tai nguyen theo ten
    public Resource GetResource (string resourceName)
    {
        return resources.Find(r => r.resourceName == resourceName); // su dung LINQ de tim tai nguyen theo ten
    }

    //them tai nguyen vao co so du lieu
    public void AddResource(string resourceName, int amount) 
    { 
        Resource resource = GetResource(resourceName); // tim tai nguyen theo ten
        if (resource != null) 
        {
            resource.AddAmount(amount); // cap nhat slg tai nguyen
            OnResourceChanged?.Invoke(resource); //goi su kien khi tai nguyen thay doi
            TriggerResourceChanged(resource); //goi thu cong de kich hoat su kien
        }
    }

    //tru tai nguyen vao co so du lieu
    public void SubtractResource(string resourceName, int amount) 
    { 
        Resource resource = GetResource(resourceName);
        if (resource != null)
        {
            resource.SubtractAmount(amount); //tru slg tai nguyen 
            OnResourceChanged?.Invoke(resource); // goi su kien khi tai nguyen thay doi
            TriggerResourceChanged(resource);
        }
    }

    public void UpgradeMaxAmountInventory(int value)
    {
        foreach (var resource in resources)
        {
            resource.UpgradeMaxAmount(value);
            TriggerResourceChanged(resource);
        }
    }

    public bool HasEnoughResources(List<ResourceUpgradeTurret> requiredResources)
    {
        foreach (var requiredResource in requiredResources)
        {
            var playerResource = GetResource(requiredResource.nameResource);
            if (playerResource == null || playerResource.amount < requiredResource.quantilyResource)
            {
                return false;
            }
        }
        return true;
    }

    public void DeductResources(List<ResourceUpgradeTurret> requiredResources)
    {
        foreach (var requiredResource in requiredResources)
        {
            SubtractResource(requiredResource.nameResource, requiredResource.quantilyResource);
        }
    }

    //ham cho phep kich hoat su kien thu cong
    public void TriggerResourceChanged(Resource resource)
    {
        OnResourceChanged?.Invoke(resource); //kich hoat su kien voi tai nguyen cu the
    }

    
}

//[ContextMenu("Save Resource")]
//public void SaveResource()
//{
//    ResourceSaveData saveData = new ResourceSaveData();
//    saveData.resources = this.resources;
//    string json = JsonUtility.ToJson(saveData, true);
//    PlayerPrefs.SetString("ResourceDatabase", json);
//    PlayerPrefs.Save();
//}

//public void LoadResource()
//{
//    string json = PlayerPrefs.GetString("ResourceDatabase", "{}");
//    ResourceSaveData saveData = JsonUtility.FromJson<ResourceSaveData>(json);
//    this.resources = saveData.resources;
//    JsonUtility.FromJsonOverwrite(json, this);
//}




