using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollectResourceState : IWorkerState
{
    private bool isCollecting = false;
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering CollectResourceState State");
        ResourceObject resource = worker.GetTargetResource();
        if (resource != null)
        {
            isCollecting = true;
            worker.workerCollectResource.StartCollecting(resource, () =>
            {
                isCollecting = false;
                worker.ChangeState(new WorkerCompleteCollectResourceState());
            });
        }
        else
        {
            worker.ChangeState(new WorkerIdleState());
        }
    }

    public void UpdateState(WorkerManager worker)
    {
        if (!isCollecting)
        {
            worker.ChangeState(new WorkerIdleState());
        }
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting CollectResourceState State");
    }
}
