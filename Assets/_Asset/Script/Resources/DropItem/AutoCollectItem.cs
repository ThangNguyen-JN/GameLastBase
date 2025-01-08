using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoCollectItem : MonoBehaviour
{
    public Transform player;
    public float moveDuration = 1f;
    public float pickupRadius = 5f;

    private bool isCollected = false;


    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isCollected == false) return;
        
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            MoveToPlayer();
        }    
    }

    private void MoveToPlayer()
    {
        transform.DOMove(player.position, moveDuration).OnComplete(() =>
        {
            CollectItem();
        });
    }

    private void CollectItem()
    {
        Debug.Log("Skull + 1");
        Destroy(gameObject);
    }
}
