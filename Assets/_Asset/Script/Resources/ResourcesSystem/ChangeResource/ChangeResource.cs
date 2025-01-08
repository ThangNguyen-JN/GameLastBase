using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeResource 
{
    private ResourceDatabase resourceDatabase;

    public ChangeResource(ResourceDatabase database)
    {
        resourceDatabase = database;
    }

    public void AddResource(string resourceName, int amount)
    {
        Resource resource = resourceDatabase.GetResource(resourceName);
        if (resource != null)
        {
            resource.AddAmount(amount);
            resourceDatabase.TriggerResourceChanged(resource);
        }
    }

    public void SubtractResource(string resourceName, int amount)
    {
        Resource resource = resourceDatabase.GetResource(resourceName);
        if (resource != null)
        {
            resource.SubtractAmount(amount);
            resourceDatabase.TriggerResourceChanged(resource);
        }
    }
}
