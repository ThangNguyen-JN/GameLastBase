using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiHealthUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCostText;
    public HealthUpdateSystem healthUpdateSystem;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinCost(healthUpdateSystem.CoinCost);
        healthUpdateSystem.CoinCostChanged += UpdateCoinCost;
        
        Debug.Log($"Coin cost: {healthUpdateSystem.CoinCost}");
    }

    public void UpdateCoinCost(int amountCost)
    {
        coinCostText.text = amountCost.ToString();
    }

    void OnDestroy()
    {
        
        if (healthUpdateSystem != null)
        {
            healthUpdateSystem.CoinCostChanged -= UpdateCoinCost;
        }
    }
}
