using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneChecker : MonoBehaviour
{
    public List<GameObject> resourcesInZone = new List<GameObject>();
    public string resourceTag;

    private void OnEnable()
    {
        ResourceObject.OnResourceDestroyed += RemoveResource;
    }

    private void OnDisable()
    {
        ResourceObject.OnResourceDestroyed -= RemoveResource;
    }

    public void AddResource(GameObject resource)
    {
        if (!resourcesInZone.Contains(resource))
        {
            resourcesInZone.Add(resource);
        }
    }

    public void RemoveResource(ResourceObject resource)
    {
        if (resource != null && resourcesInZone.Contains(resource.gameObject))
        {
            resourcesInZone.Remove(resource.gameObject);
            Debug.Log("Resource removed from list.");
        }
    }

    public void CheckResourcesInZone()
    {
        resourcesInZone.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(resourceTag))
            {
                resourcesInZone.Add(collider.gameObject);
            }
        }
        RemoveNullResource();
    }

    public bool HasResourceInSafeZone()
    {
        RemoveNullResource();
        return resourcesInZone.Count > 0;
    }
    


    public GameObject GetNearestResource(Vector3 position)
    {
        RemoveNullResource();
        if (resourcesInZone.Count == 0) return null;
        GameObject nearestResource = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject resource in resourcesInZone)
        {
            if (resource == null) continue;

            float distance = Vector3.Distance(position, resource.transform.position);
            if ( distance < minDistance)
            {
                minDistance = distance;
                nearestResource = resource;
            }
        }
        return nearestResource;
    }

    private void RemoveNullResource()
    {
        resourcesInZone.RemoveAll(resource => resource == null);
    }    
}
