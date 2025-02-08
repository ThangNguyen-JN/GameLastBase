using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMoveToStorageState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering MoveToStorageState State");
        worker.MoveToStorage(worker.GetStoredPosition());
    }

    public void UpdateState(WorkerManager worker)
    {
        if (worker.HasReachedStorage())
        {
            worker.ChangeState(new WorkerPutItemStorageState());
        }
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting MoveToStorageState State");
    }
}
