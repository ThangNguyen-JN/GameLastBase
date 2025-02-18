using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script quan ly tai nguyen thu thap va tu dong di chuyen
public class CharacterCollectItemSystem : MonoBehaviour
{
    public Transform player;
    public ChangeResource changeResource;
    public float moveDuration;

    public void Start()
    {
        changeResource = new ChangeResource();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemSkull"))
        {
            CollectItem(other.gameObject, "ItemSkull");
        }

        if (other.CompareTag("ItemWood"))
        {
            CollectItem(other.gameObject, "ItemWood");
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
            Debug.Log("Add Resource");
            //ResourceDatabase.Instance.SaveResource();
            Destroy(item);
        });
        //item.transform.DORotate(new Vector3(0, 0, 0), moveDuration, RotateMode.FastBeyond360);
        item.transform.DOScale(Vector3.zero, moveDuration);
    }


}
