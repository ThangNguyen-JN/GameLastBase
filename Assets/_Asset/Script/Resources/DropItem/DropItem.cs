using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class DropItem : MonoBehaviour
{
    [Serializable]
    public class Drop
    {
        public GameObject itemPrefab;
        public int dropQuantity;

    }   
    
    public List<Drop> drops = new List<Drop>();
    public float randomRadius = 1.5f;
    public float dropHeight = 0.6f;
    public float dropDuration = 0.2f;
    public Ease dropEase = Ease.OutBounce;

    public void DropItems(Vector3 position)
    {
        foreach (var drop in drops)
        {
            for (int i = 0; i < drop.dropQuantity; i++)
            {
                Vector3 spawnPosition = position + UnityEngine.Random.insideUnitSphere * randomRadius;
                spawnPosition.y = dropHeight;

                GameObject item = Instantiate(drop.itemPrefab, spawnPosition, Quaternion.identity);

                AnimateDrop(item.transform, 0.6f);
            }
        }
    }

    private void AnimateDrop(Transform item, float groundY)
    {
        Vector3 targetPosition = new Vector3(item.position.x, groundY, item.position.z);

        item.DOMove(targetPosition, dropDuration).SetEase(dropEase);
        item.DORotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0), dropDuration, RotateMode.FastBeyond360);
    }
}
