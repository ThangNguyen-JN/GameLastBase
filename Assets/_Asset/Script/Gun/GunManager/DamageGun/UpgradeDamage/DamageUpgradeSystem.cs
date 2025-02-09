using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgradeSystem : MonoBehaviour
{
    public event Action<int> CoinCostChangedUpDamage;
    public DamageHandler damageHandler;
    public CoinManager coinManager;
    public CoinMinusEffect coinMinusEffect;
    //public Transform playerTransformEffect;

    private int damageIncrease = 1;
    private int damageCostUpgrade = 10;
    private int currentCostUpDamage;

    public int CurrentCostUpDamage
    {
        get { return currentCostUpDamage; }
        set
        {
            currentCostUpDamage = value;
            CoinCostChangedUpDamage?.Invoke(currentCostUpDamage);
        }
    }

    private bool isPlayerInZone = false;
    private Coroutine coinUpgradeCoroutine;


    // Update is called once per frame
    void Awake()
    {
        LoadCurrentCostUpDamage();
        LoadCostUpgadeDamage();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
        {
            isPlayerInZone = true;
            if (coinUpgradeCoroutine == null)
            {
                coinUpgradeCoroutine = StartCoroutine(ReduceCoinOverTime());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUpgrade"))
        {
            isPlayerInZone = false;
            if (coinUpgradeCoroutine != null)
            {
                StopCoroutine(coinUpgradeCoroutine);
                coinUpgradeCoroutine = null;
            }
        }
    }

    private IEnumerator ReduceCoinOverTime()
    {
        yield return new WaitForSeconds(2f);

        while (CurrentCostUpDamage > 0 && isPlayerInZone == true)
        {
            if (coinManager.Coin > 0)
            {
                coinManager.SpendCoin(1);
                CurrentCostUpDamage -= 1;
                coinMinusEffect.SpawnCoinEffect();
                SaveCurrentCostUpDamage();

            }
            else
            {
                Debug.Log("Not enough coin");
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }

        if (CurrentCostUpDamage <= 0)
        {

            UpgradeDamage();

        }
    }

    public void UpgradeDamage()
    {
        damageHandler.UpgradeDamageGun(damageIncrease);
        damageCostUpgrade += 10;
        SaveCostUpgradeDamage();
        CurrentCostUpDamage = damageCostUpgrade;
        SaveCurrentCostUpDamage();

    }

    public void SaveCurrentCostUpDamage()
    {
        PlayerPrefs.SetInt("CurrentCostUpDamage", currentCostUpDamage);
        PlayerPrefs.Save();
    }

    public void LoadCurrentCostUpDamage()
    {
        currentCostUpDamage = PlayerPrefs.GetInt("CurrentCostUpDamage", 10);
    }


    // Luu gia tien nang cap
    public void SaveCostUpgradeDamage()
    {
        PlayerPrefs.SetInt("CostUpdateDamage", damageCostUpgrade);
        PlayerPrefs.Save();
    }

    public void LoadCostUpgadeDamage()
    {
        damageCostUpgrade = PlayerPrefs.GetInt("CostUpdateDamage", 10);
    }

    public void OnApplicationQuit()
    {
        SaveCurrentCostUpDamage();
        SaveCostUpgradeDamage();
    }
}
