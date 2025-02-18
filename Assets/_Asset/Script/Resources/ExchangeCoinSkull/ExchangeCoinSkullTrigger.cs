using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCoinSkullTrigger : MonoBehaviour
{
    //public event Action<int> ExchangeCoinSkull;
    public bool playerInTrigger;
   

    private ResourceDatabase resourceDatabase;
    public CoinManager coinManager;

    private void Start()
    {
        resourceDatabase = ResourceDatabase.Instance;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            StartCoroutine(ExchangeCoinSkullOverTime());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            StopCoroutine(ExchangeCoinSkullOverTime());
        }
    }

    private IEnumerator ExchangeCoinSkullOverTime()
    {
        yield return new WaitForSeconds(2f);

        while (playerInTrigger)
        {
            Resource skullResource = resourceDatabase.GetResource("ItemSkull");
            if (skullResource != null && skullResource.amount > 0)
            {
                resourceDatabase.SubtractResource("ItemSkull", 1);
                coinManager.AddCoin(1);
                //ExchangeCoinSkull?.Invoke(1);

                //resourceDatabase.SaveResource();
                coinManager.SaveCoin();
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Debug.Log("Not enough skull");
                break;
            }
        }
    }
}
