using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpdateSystem : MonoBehaviour
{
    public event Action<int> CoinCostChangedUpDamage;
    public DamageHandler damageHandler;
    public CoinManager coinManager;
    public CoinMinusEffect coinMinusEffect;
    //public Transform playerTransformEffect;

    private int damageIncrease = 1;
    private int damageCostUpdate = 10;
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
    private Coroutine coinUpdateCoroutine;


    // Update is called once per frame
    void Awake()
    {
        LoadCurrentCostUpDamage();
        LoadCostUpdateDamage();
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

            UpdateHealth();

        }
    }

    public void UpdateHealth()
    {
        damageHandler.UpdateDamageGun(damageIncrease);
        damageCostUpdate += 10;
        SaveCostUpdateDamage();
        CurrentCostUpDamage = damageCostUpdate;
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
    public void SaveCostUpdateDamage()
    {
        PlayerPrefs.SetInt("CostUpdateDamage", damageCostUpdate);
        PlayerPrefs.Save();
    }

    public void LoadCostUpdateDamage()
    {
        damageCostUpdate = PlayerPrefs.GetInt("CostUpdateDamage", 10);
    }

    public void OnApplicationQuit()
    {
        SaveCurrentCostUpDamage();
        SaveCostUpdateDamage();
    }
}
