using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Resource
{
    public string resourceName;
    public Sprite iconPath;
    public int amount;
    public int maxAmount;
    public bool unlock = false;

    public void AddAmount(int value)
    {
        amount = Mathf.Clamp(amount + value, 0, maxAmount);
    }

    public void SubtractAmount(int value)
    {
        amount = Mathf.Clamp(amount - value, 0, maxAmount);
    }

    public void UpgradeMaxAmount (int value)
    {
        maxAmount += value;
    }

    
}

[Serializable]
public class ResourceSaveData
{
    public List<Resource> resources =  new List<Resource>();
}