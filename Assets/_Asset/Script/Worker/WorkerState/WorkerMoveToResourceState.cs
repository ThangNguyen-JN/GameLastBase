using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMoveToResourceState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering MoveToResource State");
        worker.MoveToResource(worker.GetNearestResourcePosition());
    }

    public void UpdateState(WorkerManager worker)
    {
        if (worker.HasReachedDestination())
        {
            Debug.Log("Worker completed move to resource");
            worker.ChangeState(new WorkerCollectResourceState());
            return;
        }

        if (worker.HasResourceInSafeZone()) return;
        else
        {
            worker.ChangeState(new WorkerIdleState());
        }
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting MoveToResource State");
    }
}
