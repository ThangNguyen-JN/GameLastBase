using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkerCollectResource : MonoBehaviour
{
    public Animator anim;
    private bool isCollecting = false;
    private GameObject targetResource;
    private bool isResourceDestroyed = false;
    // Start is called before the first frame update
    public void StartCollecting(GameObject resource)
    {
        targetResource = resource;
        anim.SetBool("isCollecting", true);
        isCollecting = true;
        isResourceDestroyed = false;
    }

    public void EndCollecting()
    {
        anim.SetBool("isCollecting", false);
        isCollecting = false;
        targetResource = null;
    }   

    public void OnHarvestEventTriggered()
    {
 
        ResourceObject resourceObject = targetResource.GetComponentInParent<ResourceObject>();
        if (resourceObject != null)
        {
            resourceObject.Harvest();
            if (resourceObject.resourceCount <= 0)
            {
                isResourceDestroyed = true;
                if (isResourceDestroyed == true)
                {
                    EndCollecting();
                }
            }

        }
    }

    public bool HasFinishedCollecting()
    {
        if (isCollecting)
        {
            return false; 
        }
        else
        {
            return true; 
        }
    }

    public bool IsResourceDestroy()
    {
        return isResourceDestroyed;
    }


}
