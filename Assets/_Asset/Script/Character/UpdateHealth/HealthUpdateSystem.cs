using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdateSystem : MonoBehaviour
{
    public CharacterManager characterManager;
    public CoinManager coinManager;

    private int healthIncrease = 15;
    private int coinCostFirst = 15;
    private int coinCost;

    private bool isPlayerInZone = false;
    private Coroutine coinUpdateCoroutine;
   

    // Update is called once per frame
    void Start()
    {
        LoadCostUPHealth();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpdateHealthZone"))
        {
            isPlayerInZone = true;
            if (coinUpdateCoroutine == null)
            {
                coinUpdateCoroutine = StartCoroutine(ReduceCoinOverTime());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpdateHealthZone"))
        {
            isPlayerInZone = false;
            StopCoroutine(coinUpdateCoroutine);
            coinUpdateCoroutine = null;
        }
    }


    

    private IEnumerator ReduceCoinOverTime()
    {
        while(coinCost >0 && isPlayerInZone == true)
        {
            if (coinManager.Coin > 0)
            {
                coinManager.SpendCoin(1);
                coinCost -= 1;
                SaveCostUPHealth();

            }
            else
            {
                Debug.Log("Not enough coin");
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        

        if (coinCost <= 0)
        {
            
            UpdateHealth();
            
        }
        
    }

    public void UpdateHealth()
    {
        characterManager.UpdateMaxHealth(healthIncrease);
        coinCostFirst += 5;
        coinCost = coinCostFirst;
        SaveCostUPHealth();
    }

    public void SaveCostUPHealth()
    {
        PlayerPrefs.SetInt("CostUPHealth", coinCost);
        PlayerPrefs.Save();
    }

    public void LoadCostUPHealth()
    {
        coinCost = PlayerPrefs.GetInt("CostUPHealth", 15);
    }

    public void OnApplicationQuit()
    {
        SaveCostUPHealth();
    }



}
