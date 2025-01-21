using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDamageUpdate : MonoBehaviour
{
    [SerializeField] private Text coinCostText;
    public DamageUpgradeSystem damageUpdateSystem;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinCost(damageUpdateSystem.CurrentCostUpDamage);
        damageUpdateSystem.CoinCostChangedUpDamage += UpdateCoinCost;

        Debug.Log($"Coin cost: {damageUpdateSystem.CurrentCostUpDamage}");
    }

    public void UpdateCoinCost(int amountCost)
    {
        coinCostText.text = amountCost.ToString();
    }

    void OnDestroy()
    {

        if (damageUpdateSystem != null)
        {
            damageUpdateSystem.CoinCostChangedUpDamage -= UpdateCoinCost;
        }
    }
}
