using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageQuantity : MonoBehaviour
{
    public event Action<int, int> ChangedCurrentResourceStorage;
    public Sprite imageStorage;
    [SerializeField] private int currentResource;
    public int maxResource;
    public int CurrentResource
    {
        get { return currentResource; }
        set
        {
            currentResource = Mathf.Clamp(value, 0, maxResource);
            Debug.Log($"[StorageQuantity] CurrentResource set - Current: {currentResource}, Max: {maxResource}");
            ChangedCurrentResourceStorage?.Invoke(CurrentResource, maxResource);
        }
    }
   
    public void AddResource(int amount)
    {
        CurrentResource += amount;
    }

    public void MinusResource(int amount)
    {
        CurrentResource -= amount;
    }

    public void UpgradeMaxResource(int amount)
    {
        maxResource += amount;
        ChangedCurrentResourceStorage?.Invoke(CurrentResource, maxResource);
    }

   
}




