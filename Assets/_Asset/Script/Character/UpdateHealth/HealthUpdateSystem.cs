using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdateSystem : MonoBehaviour
{
    
    public event Action<int> CoinCostChanged;
    public CharacterManager characterManager;
    public CoinManager coinManager;

    private int healthIncrease = 15;
    private int coinCostFirst = 15;
    private int coinCost;

    public int CoinCost
    { 
        get { return coinCost; }
        set
        {
            coinCost = value;
            CoinCostChanged?.Invoke(coinCost);
        }
    }

    private bool isPlayerInZone = false;
    private Coroutine coinUpdateCoroutine;
   

    // Update is called once per frame
    void Awake()
    {
        LoadCostUPHealth();
        Debug.Log($"coinCost: {coinCost}");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            if (coinUpdateCoroutine != null)
            {
                StopCoroutine(coinUpdateCoroutine);
                coinUpdateCoroutine = null;
            }
        }
    }


    

    private IEnumerator ReduceCoinOverTime()
    {
        yield return new WaitForSeconds(2f);

        while(CoinCost > 0 && isPlayerInZone == true)
        {
            if (coinManager.Coin > 0)
            {
                coinManager.SpendCoin(1);
                CoinCost -= 1;
                SaveCostUPHealth();

            }
            else
            {
                Debug.Log("Not enough coin");
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }

        

        if (CoinCost <= 0)
        {
            
            UpdateHealth();
            
        }
        
    }

    public void UpdateHealth()
    {
        characterManager.UpdateMaxHealth(healthIncrease);
        coinCostFirst += 5;
        CoinCost = coinCostFirst;
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
