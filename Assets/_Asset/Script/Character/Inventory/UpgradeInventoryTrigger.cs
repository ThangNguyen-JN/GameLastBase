using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInventoryTrigger : MonoBehaviour
{
    public event Action<int> CoinCostChangedUpInventory;
    public bool isInZone = false;
    public CoinManager coinManager;
    public CoinMinusEffect coinMinusEffect;

    private int inventoryIncrease = 5;
    private int inventoryCostUpgrade = 20;
    private int currentCostUpInventory = 20;


    public void Awake()
    {
        LoadCurrentCostUpInventory();
        LoadCostUpgradeInventory();
    }
    public int CurrentCostUpInventory
    {
        get { return currentCostUpInventory; }
        set
        {
            currentCostUpInventory = value;
            CoinCostChangedUpInventory?.Invoke(currentCostUpInventory);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
        {
            isInZone = true;
            StartCoroutine(ReduceCoinOverTime());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
        {
            isInZone = false;
            StopCoroutine(ReduceCoinOverTime());
        }
    }

    private IEnumerator ReduceCoinOverTime()
    {
        yield return new WaitForSeconds(2f);
        while (CurrentCostUpInventory > 0 && isInZone == true)
        {
            if (coinManager.Coin > 0)
            {
                coinManager.SpendCoin(1);
                CurrentCostUpInventory -= 1;
                coinMinusEffect.SpawnCoinEffect();
            }
            else
            {
                Debug.Log("Enough coin upgrade");
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        if (CurrentCostUpInventory <= 0)
        {
            UpgradeInventory();
        }
    }

    private void UpgradeInventory()
    {
        ResourceDatabase resourceData = ResourceDatabase.Instance;
        resourceData.UpgradeMaxAmountInventory(inventoryIncrease);
        resourceData.SaveResource();
        inventoryCostUpgrade += 30;
        SaveCostUpgradeInventory();
        CurrentCostUpInventory = inventoryCostUpgrade;
        SaveCurrentCostUpInventory();
    }

    private void SaveCurrentCostUpInventory()
    {
        PlayerPrefs.SetInt("CurrentCostUpInventory", currentCostUpInventory);
        PlayerPrefs.Save();
    }

    public void LoadCurrentCostUpInventory()
    {
        currentCostUpInventory = PlayerPrefs.GetInt("CurrentCostUpInventory", 20);
    }

    public void SaveCostUpgradeInventory()
    {
        PlayerPrefs.SetInt("CostUpInventory", inventoryCostUpgrade);
        PlayerPrefs.Save();
    }

    public void LoadCostUpgradeInventory()
    {
        inventoryCostUpgrade = PlayerPrefs.GetInt("CostUpInventory", 20);
    }

    public void OnApplicationQuit()
    {
        SaveCurrentCostUpInventory();
        SaveCostUpgradeInventory();
    }
}
