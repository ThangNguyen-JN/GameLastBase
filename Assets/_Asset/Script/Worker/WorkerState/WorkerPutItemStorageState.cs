using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPutItemStorageState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering StoreResourceState State");
        worker.StartCoroutine(MinusResource(worker));
    }

    public void UpdateState(WorkerManager worker)
    {

    }


    public IEnumerator MinusResource(WorkerManager worker)
    {

        yield return new WaitForSeconds(1f);

        int maxTries = worker.quantityResource.QuantityResource;
        int tries = 0;

        while (worker != null && worker.quantityResource.QuantityResource > 0 && tries < maxTries)
        {
            yield return new WaitForSeconds(0.2f);
            if (worker == null) yield break;
            worker.quantityResource.MinusResourceWorker(1);
            worker.storageQuantity.AddResource(1);
            tries++;
        }

        worker.ChangeState(new WorkerIdleState());
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting StoreResourceState State");
    }
}

