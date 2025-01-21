using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInZoneResource : MonoBehaviour
{
    public event Action<bool> PlayerInZoneResource;
    public CharacterStateManager stateManager;
    private bool isZoneResource = false;
    public GameObject axe;
    public GameObject axeBackPack;
    public GameObject gun;

    public List<GameObject> resourcesInZone = new List<GameObject>();
    //public GameObject miningTool;
    public bool IsZoneResource
    {
        get { return isZoneResource; }
        set
        {
            if (isZoneResource != value)
            {
                isZoneResource = value;
                PlayerInZoneResource?.Invoke(isZoneResource); 
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZoneResource"))
        {
            resourcesInZone.Add(other.gameObject);
            stateManager.SetClosestResource(GetClosestResource());
            IsZoneResource = true;
            
            if (IsZoneResource == true)
            {
                axe.SetActive(true);
                gun.SetActive(false);
                axeBackPack.SetActive(false);
            }     
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZoneResource") && resourcesInZone.Contains(other.gameObject))
        {
            resourcesInZone.Remove(other.gameObject);
            stateManager.SetClosestResource(GetClosestResource());
            IsZoneResource = resourcesInZone.Count > 0;
            IsZoneResource = false;

            if (IsZoneResource == false)
            {
                ChangedTool();
            }
        }
    }

    public void ChangedTool()
    {
        axe.SetActive(false);
        gun.SetActive(true);
        axeBackPack.SetActive(true);
    }    

    public GameObject GetClosestResource()
    {
        GameObject closestResource = null;
        float closestDistance = Mathf.Infinity;

        resourcesInZone.RemoveAll(resource => resource == null);

        foreach (var resource in resourcesInZone)
        {
            float distance = Vector3.Distance(resource.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestResource = resource;
            }
        }
        return closestResource;
    }
}
