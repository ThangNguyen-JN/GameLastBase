using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;
    public SafeZoneChecker safeZoneChecker;
    private GameObject currentResource;
    public List<Transform> spawnPoints;
    private Dictionary<Transform, GameObject> spawnedResources = new Dictionary<Transform, GameObject>();

    public void Start()
    {
        SpawnAllResource();
    }

    public void SpawnAllResource()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnResource(spawnPoint);
        }
        safeZoneChecker.CheckResourcesInZone();
    }
    public void SpawnResource(Transform spawnPoint)
    {
        if (spawnedResources.ContainsKey(spawnPoint) && spawnedResources[spawnPoint] != null)
        {
            Destroy(spawnedResources[spawnPoint]);
        }

        GameObject newResource = Instantiate(resourcePrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedResources[spawnPoint] = newResource;
    }

    public void DestroyResourceAt(Transform spawnPoint)
    {
        if (spawnedResources.ContainsKey(spawnPoint) && spawnedResources[spawnPoint] != null)
        {
            Destroy(spawnedResources[spawnPoint]);
            spawnedResources.Remove(spawnPoint); 
        }

        safeZoneChecker.CheckResourcesInZone();
    }

    public void ClearAllResources()
    {
        foreach (var resource in spawnedResources.Values)
        {
            if (resource != null)
            {
                Destroy(resource);
            }
        }

        spawnedResources.Clear();
        safeZoneChecker.CheckResourcesInZone();
    }
}
