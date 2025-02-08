using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollectItemSystem : MonoBehaviour
{
    public Transform worker;
    public QuantityResourceWorker quantityResourceWorker;
    public string tagItem;
    public float moveDuration;
    public WorkerManager workerManager;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(tagItem))
        {
            if (quantityResourceWorker.QuantityResource >= quantityResourceWorker.maxQuantity)
            {
                workerManager.ChangeState(new WorkerMoveToStorageState());
            }
            else
            {
                CollectItem(other.gameObject);
            }
        }
    }


    private void CollectItem(GameObject item)
    {
        Sequence moveSequence = DOTween.Sequence();

        moveSequence.AppendCallback(() =>
        {
            item.transform.DOMove(worker.position, moveDuration).SetEase(Ease.Linear);
        });

        moveSequence.AppendInterval(moveDuration).OnComplete(() =>
        {
            quantityResourceWorker.AddResourceWorker(1);
            //ResourceDatabase.Instance.SaveResource();
            Destroy(item);
            if (quantityResourceWorker.QuantityResource >= quantityResourceWorker.maxQuantity)
            {
                workerManager.ChangeState(new WorkerMoveToStorageState());
            }


        });
        //item.transform.DORotate(new Vector3(0, 0, 0), moveDuration, RotateMode.FastBeyond360);
        item.transform.DOScale(Vector3.zero, moveDuration);
    }

}
