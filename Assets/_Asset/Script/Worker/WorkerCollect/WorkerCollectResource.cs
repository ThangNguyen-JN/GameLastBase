using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkerCollectResource : MonoBehaviour
{
    public Animator anim;
    private bool isCollecting = false;
    private Action onCollectComplete;
    // Start is called before the first frame update
    public void StartCollecting(ResourceObject resource, Action callback)
    {
        if (isCollecting || resource == null || resource.resourceCount <= 0)
        {
            callback?.Invoke();
            return;
        }

        isCollecting = true;
        onCollectComplete = callback;

        if (anim != null)
        {
            anim.SetBool("isCollecting", true);
        }

        StartCoroutine(CollectResource(resource));
    }

    private IEnumerator CollectResource(ResourceObject resource)
    {
        while (resource.resourceCount > 0)
        {
            yield return new WaitForSeconds(0.5f);
            resource.Harvest();
        }

        isCollecting = false;
        anim?.SetBool("isCollecting", false);
        onCollectComplete?.Invoke();
    }
}
