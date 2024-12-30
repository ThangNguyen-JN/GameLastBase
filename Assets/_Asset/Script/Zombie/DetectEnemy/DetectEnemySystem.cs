using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemySystem : MonoBehaviour
{
    public enum TriggerType { Chase, Attack}
    public TriggerType triggerType;
    public event Action<Transform> onEnemyDetected;
    public event Action onEnemyLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Chase) 
            {
                onEnemyDetected?.Invoke(other.transform);
            }
            else if (triggerType == TriggerType.Attack)
            {
                onEnemyDetected?.Invoke(other.transform);
            }
            
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Chase)
            {
                onEnemyLost?.Invoke();
            }
            else if (triggerType == TriggerType.Attack)
            {
                onEnemyLost?.Invoke();
            }
        }
    }
}
