using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemSystem : MonoBehaviour
{
    public float moveDuration = 1f;
    public Transform player;
    public ResourceDatabase resourceDatabase;
    public ChangeResource changeResource;

    public void Start()
    {
        changeResource = new ChangeResource(resourceDatabase);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemSkull"))
        {
            CollectItem(other.gameObject, "ItemSkull");
        }    
    }

    private void CollectItem(GameObject item, string resourceName)
    {
        Sequence moveSequence = DOTween.Sequence();

        moveSequence.AppendCallback(() =>
        {
            item.transform.DOMove(player.position, moveDuration).SetEase(Ease.Linear);
        });

        moveSequence.AppendInterval(moveDuration).OnComplete(() =>
        {
            changeResource.AddResource(resourceName, 1);
            resourceDatabase.SaveResource();
            Destroy(item);
        });
        item.transform.DORotate(new Vector3(0, 360, 0), moveDuration, RotateMode.FastBeyond360);
        item.transform.DOScale(Vector3.zero, moveDuration);
    }

    
}
