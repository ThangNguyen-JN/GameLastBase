using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneChecker : MonoBehaviour
{
    public List<GameObject> resourcesInZone = new List<GameObject>();
    public string resourceTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag (resourceTag))
        {
            resourcesInZone.Add (other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(resourceTag))
        {
            resourcesInZone.Remove(other.gameObject);
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
    }

    public bool HasResourceInSafeZone()
    {
        return resourcesInZone.Count > 0;
    }
    


    public GameObject GetNearestResource(Vector3 position)
    {
        GameObject nearestResource = null;
        float minDistance = float.MaxValue;

        foreach (var resource in resourcesInZone)
        {
            float distance = Vector3.Distance(position, resource.transform.position);
            if ( distance < minDistance)
            {
                minDistance = distance;
                nearestResource = resource;
            }
        }
        return nearestResource;
    }
}
