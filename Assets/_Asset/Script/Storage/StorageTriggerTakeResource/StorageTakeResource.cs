using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class StorageTakeResource : MonoBehaviour
{
    public string storageResource;
    public StorageQuantity storageQuantity;
    
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTakeResource"))
        {
            StartCoroutine(PlayerTakeResource());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerTakeResource"))
        {
            StopCoroutine(PlayerTakeResource());
            StorageUpgradeManage.Instance.SaveStorageData();
        }    
    }

    public IEnumerator PlayerTakeResource()
    {
        yield return new WaitForSeconds(2f);
        

        while (storageQuantity.CurrentResource > 0)
        {
            Resource resource = ResourceDatabase.Instance.GetResource(storageResource);
            Debug.Log($"Resource Name: {storageResource}");
            if (resource != null && resource.amount >= resource.maxAmount)
            {
                yield break;
            }    
            ResourceDatabase.Instance.AddResource(storageResource, 1);
            storageQuantity.MinusResource(1);
            yield return new WaitForSeconds(0.2f);
        }    
    }    
}
