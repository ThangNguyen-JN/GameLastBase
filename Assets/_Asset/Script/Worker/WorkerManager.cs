using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class WorkerManager : MonoBehaviour
{
    public SafeZoneChecker safeZoneChecker;
    public WorkerMovement workerMovement;
    public WorkerCollectResource workerCollectResource;
    private IWorkerState currentState;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new WorkerIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void ChangeState (IWorkerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    
    public bool HasResourceInSafeZone() //kiem tra t/nguyen trong k/vuc an toan
    {
        return safeZoneChecker.HasResourceInSafeZone();
    }
    
    public bool StoreHasSpace() // kiem tra kho con cho chua khong
    {
        return true;
    }

    public GameObject GetNearestResourcePosition() // tim t/nguyen vi tri gan nhat
    {
        return safeZoneChecker.GetNearestResource(transform.position);
    }

    public void MoveToResource(GameObject resource) // di chuyen den vi tri t/nguyen
    {
        workerMovement.MoveToResource(resource);
    }

    public bool HasReachedDestination()
    {
        return workerMovement.HasReachedDestination();
    }

    public void StartCollecting() // bat dau thu thap tai nguyen
    {
        GameObject nearestResource = GetNearestResourcePosition();
        if (nearestResource != null)
        {
            workerCollectResource.StartCollecting(nearestResource);
            Debug.Log("Start Collecting");
        }
    }

    public void HarvestEventTriggered()
    {
        workerCollectResource.OnHarvestEventTriggered();
        Debug.Log("Harvest Event Triggered");
    }    

    public void EndCollecting()
    {
        workerCollectResource.EndCollecting();
    }

    public bool IsResourceDestroy()
    {
        return workerCollectResource.IsResourceDestroy();
    }

    // test


    public bool HasCollectedResource() //ktra thu thap xong chua
    {
        return workerCollectResource.HasFinishedCollecting();
    }

    public Vector3 GetStoredPosition() // lay vi tri kho chua
    {
        return Vector3.zero;
    }

    public void StoreResource() // cat tai nguyen vao kho
    {

    }

    public bool HasStoredResource() // ktra cat xong chua
    {
        return true;
    }

   
}
