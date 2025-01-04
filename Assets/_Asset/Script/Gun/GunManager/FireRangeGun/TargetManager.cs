using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private List<GameObject> targetsInRange = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.gameObject);
            Debug.Log($"Target Entered {other.gameObject}");
        }    
    }

    public void OnTriggerExit(Collider other)
    {
        if (targetsInRange.Contains(other.gameObject))
        {
            targetsInRange.Remove(other.gameObject);
        }
    }

    public GameObject FindClosestTarget(Vector3 currentPosition)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestTarget = null;

        foreach (var target in targetsInRange) 
        { 
            
            if (target == null)
            {
                continue;
            }
            float distance = Vector3.Distance(currentPosition, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
        
        return closestTarget;
        
    }
    public bool HasTargets()
    {
        return targetsInRange.Count > 0;
    }
}
