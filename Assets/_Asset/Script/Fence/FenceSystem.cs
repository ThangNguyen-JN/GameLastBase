using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSystem : MonoBehaviour
{
   
    public GameObject fenceLaser;
    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFence") || other.CompareTag("Worker"))
        {
            fenceLaser.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerFence") || other.CompareTag("Worker"))
        {
            fenceLaser.SetActive(true);

        }
    }
}
