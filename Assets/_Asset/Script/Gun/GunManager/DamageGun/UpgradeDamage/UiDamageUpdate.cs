using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDamageUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCostText;
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
