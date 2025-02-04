using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreResourceState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering StoreResourceState State");
    }

    public void UpdateState(WorkerManager worker)
    {

    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting StoreResourceState State");
    }
}
