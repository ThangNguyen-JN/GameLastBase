using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCompleteCollectResourceState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Enter WorkerCompleteCollectResourceState State");
        GameObject nextResource = worker.GetNearestResourcePosition();
        if (nextResource != null)
        {
            worker.ChangeState(new WorkerMoveToResourceState());
        }
        else
        {
            worker.ChangeState(new WorkerIdleState());
        }
    }
    public void UpdateState(WorkerManager worker)
    {
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting WorkerCompleteCollectResourceState State");
    }
}
