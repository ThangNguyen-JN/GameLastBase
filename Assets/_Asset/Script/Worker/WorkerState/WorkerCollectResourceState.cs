using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollectResourceState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Exiting CollectResourceState State");
        worker.StartCollecting();
    }

    public void UpdateState(WorkerManager worker)
    {
        if (worker.HasCollectedResource() == true)
        {
            if (worker.IsResourceDestroy() == true)

            {
                worker.ChangeState(new WorkerCompleteCollectResourceState());
            }
        }
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting CollectResourceState State");
        worker.EndCollecting();
    }
}
