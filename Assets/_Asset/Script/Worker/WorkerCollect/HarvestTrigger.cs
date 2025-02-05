using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTrigger : MonoBehaviour
{
    public WorkerManager workerManager;

    public void OnHarvestTriggerResource()
    {
        workerManager.HarvestEventTriggered();
        Debug.Log("Event Harvest");
    }
}
