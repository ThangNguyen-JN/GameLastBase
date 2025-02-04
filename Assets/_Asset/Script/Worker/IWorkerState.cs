using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkerState
{
    void EnterState(WorkerManager worker);
    void UpdateState(WorkerManager worker);
    void ExitState(WorkerManager worker);
}
