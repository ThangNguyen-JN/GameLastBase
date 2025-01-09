using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeResource 
{

    public void AddResource(string resourceName, int amount)
    {
        Resource resource = ResourceDatabase.Instance.GetResource(resourceName);
        if (resource != null)
        {
            resource.AddAmount(amount);
            ResourceDatabase.Instance.TriggerResourceChanged(resource);
        }
    }

    public void SubtractResource(string resourceName, int amount)
    {
        Resource resource = ResourceDatabase.Instance.GetResource(resourceName);
        if (resource != null)
        {
            resource.SubtractAmount(amount);
            ResourceDatabase.Instance.TriggerResourceChanged(resource);
        }
    }
}
