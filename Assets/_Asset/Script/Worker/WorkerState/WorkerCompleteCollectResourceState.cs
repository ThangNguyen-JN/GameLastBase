using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCompleteCollectResourceState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Enter WorkerCompleteCollectResourceState State");
    }
    public void UpdateState(WorkerManager worker)
    {
        
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting WorkerCompleteCollectResourceState State");
    }
}
