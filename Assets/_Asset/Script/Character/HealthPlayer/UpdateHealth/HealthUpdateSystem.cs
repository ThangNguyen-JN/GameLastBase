using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdateSystem : MonoBehaviour
{
    
    public event Action<int> CoinCostChanged;
    public HealthPlayer characterManager;
    public CoinManager coinManager;
    public CoinMinusEffect coinMinusEffect;
    //public Transform playerTransformEffect;

    private int healthIncrease = 15;
    private int coinCostUpdate = 15;
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
        LoadCostUpdateHealth();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
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
        if (other.CompareTag("PlayerUpgrade"))
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
                coinMinusEffect.SpawnCoinEffect();
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
        coinCostUpdate += 5;
        SaveCostUpdateHealth();
        CoinCost = coinCostUpdate;
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


    // Luu gia tien nang cap
    public void SaveCostUpdateHealth()
    {
        PlayerPrefs.SetInt("CostUpdateHealth", coinCostUpdate);
        PlayerPrefs.Save();
    }

    public void LoadCostUpdateHealth()
    {
        coinCostUpdate = PlayerPrefs.GetInt("CostUpdateHealth", 15);
    }

    public void OnApplicationQuit()
    {
        SaveCostUPHealth();
        SaveCostUpdateHealth();
    }


}
