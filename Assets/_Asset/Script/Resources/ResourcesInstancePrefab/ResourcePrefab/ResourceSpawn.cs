using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject treePrefab; // Prefab cây sẽ hồi sinh
    public Transform parent;
    public float respawnTime = 20f;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
    }
    public void StartRespawn()
    {
        StartCoroutine(RespawnTree());
    }

    private IEnumerator RespawnTree()
    {
        Debug.Log("Respawn tree");
        yield return new WaitForSeconds(respawnTime);
        Instantiate(treePrefab, spawnPosition, Quaternion.identity, parent);
    }
}
