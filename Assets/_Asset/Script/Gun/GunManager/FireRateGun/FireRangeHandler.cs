using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangeHandler : MonoBehaviour
{
    [SerializeField] private float fireRange;

    public float FireRange
    {
        get { return fireRange; }
        set { fireRange = Mathf.Max(value, 1f); }
    }
    // Start is called before the first frame update
    
    public bool IsTargetInRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= FireRange;
    }
}
