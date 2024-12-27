using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemySystem : MonoBehaviour
{
    public event Action<Transform> onEnemyDetected;
    public event Action onEnemyLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onEnemyDetected?.Invoke(other.transform);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onEnemyLost?.Invoke(); 
        }
    }
}
