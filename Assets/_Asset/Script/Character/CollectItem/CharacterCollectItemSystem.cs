using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script quan ly tai nguyen thu thap va tu dong di chuyen
public class CharacterCollectItemSystem : MonoBehaviour
{
    public float moveDuration = 1f;
    public Transform player;
    public ChangeResource changeResource;

    public void Start()
    {
        changeResource = new ChangeResource();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemWood") )
        {
            ItemCollectable item = other.GetComponent<ItemCollectable>();
            if (item != null)
            {
                item.MoveToTarget(player); // Gửi tài nguyên đến CharacterMain
                ChangeCollectItem("ItemWood");
            }
        }
        if (other.CompareTag("ItemSkull"))
        {
            ItemCollectable item = other.GetComponent<ItemCollectable>();
            if (item != null)
            {
                item.MoveToTarget(player); // Gửi tài nguyên đến CharacterMain
                ChangeCollectItem("ItemSkull");
            }
        }
    }

    private void ChangeCollectItem(string resourceName)
    {
        changeResource.AddResource(resourceName, 1);
        ResourceDatabase.Instance.SaveResource();
    }    
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("ItemSkull"))
    //    {
    //        CollectItem(other.gameObject, "ItemSkull");
    //    }    

    //    if (other.CompareTag("ItemWood"))
    //    {
    //        CollectItem(other.gameObject, "ItemWood");
    //    }    
    //}


    //private void CollectItem(GameObject item, string resourceName)
    //{
    //    Sequence moveSequence = DOTween.Sequence();

    //    moveSequence.AppendCallback(() =>
    //    {
    //        item.transform.DOMove(player.position, moveDuration).SetEase(Ease.Linear);
    //    });

    //    moveSequence.AppendInterval(moveDuration).OnComplete(() =>
    //    {
    //        changeResource.AddResource(resourceName, 1);
    //        ResourceDatabase.Instance.SaveResource();
    //        Destroy(item);
    //    });
    //    item.transform.DORotate(new Vector3(0, 360, 0), moveDuration, RotateMode.FastBeyond360);
    //    item.transform.DOScale(Vector3.zero, moveDuration);
    //}


}
