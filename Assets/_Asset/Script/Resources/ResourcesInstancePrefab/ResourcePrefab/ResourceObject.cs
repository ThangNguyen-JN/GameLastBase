using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public int resourceCount = 3;
    public float dropRadius = 2f;
    public GameObject dropPrefab;
    public Transform dropPosition;

    public GameObject treeResource;
    public GameObject treeFull;
    public GameObject tree2;
    public GameObject tree1;
    public GameObject tree0;

    public void Harvest()
    {
        if (resourceCount > 0)
        {
            Debug.Log($"Resource Count:{resourceCount}");
            resourceCount--;
            SpawnResource();

            if (resourceCount == 2)
            {
                treeFull.SetActive(false);
                tree2.SetActive(true);
            }
            else if (resourceCount == 1)
            {
                treeFull.SetActive(false);
                tree2.SetActive(false);
                tree1.SetActive(true);
            }
            else if (resourceCount <= 0)
            {
                Destroy(treeResource);
                
                tree0.SetActive(true);
            }
           
        }
    }

    private void SpawnResource()
    {
        if (dropPrefab != null && dropPosition != null)
        {
            Vector2 randomCircle = Random.insideUnitCircle * dropRadius;
            Vector3 randomPosition = dropPosition.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            Instantiate(dropPrefab, randomPosition, Quaternion.identity);
        }
    }
}
