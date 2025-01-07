using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectItem : MonoBehaviour
{
    public GameObject itemPrefab;
    
    public float speedMove;
    public Transform targetPosition;
    public bool isCollect = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCollect = true;
        }
    }

    private void Update()
    {
        if (isCollect == true)
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, speedMove * Time.deltaTime);
    }
}
