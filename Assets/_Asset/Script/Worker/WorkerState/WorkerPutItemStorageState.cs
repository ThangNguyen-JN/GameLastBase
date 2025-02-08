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
        while (worker.quantityResource.QuantityResource > 0)
        {
            yield return new WaitForSeconds(0.2f);
            worker.quantityResource.MinusResourceWorker(1);
        }

        worker.ChangeState(new WorkerIdleState());
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting StoreResourceState State");
    }
}

