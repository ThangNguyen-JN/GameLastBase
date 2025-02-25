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
        StartCoroutine(SpawnAllResourcesCoroutine());
    }

    private IEnumerator SpawnAllResourcesCoroutine()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnResource(spawnPoint);
            yield return new WaitForSeconds(0.15f); 
        }

        if (safeZoneChecker != null)
        {
            safeZoneChecker.CheckResourcesInZone();
        }
    }
    public void SpawnResource(Transform spawnPoint)
    {
        if (resourcePrefab == null)
        {
            return;
        }
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
