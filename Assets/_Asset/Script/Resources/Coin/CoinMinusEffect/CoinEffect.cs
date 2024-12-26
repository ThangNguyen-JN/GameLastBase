
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 3f;
    private float initialScale = 1f;
    private float minScale = 0.3f;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        transform.localScale = Vector3.one * initialScale;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPosition);
        float totalDistance = Vector3.Distance(transform.position, targetPosition + Vector3.one * speed);

        float scale = Mathf.Lerp(minScale, initialScale, distance / totalDistance);
        transform.localScale = Vector3.one * scale; 
        
        if (distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
