using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUpgradeInventory : MonoBehaviour
{
    [SerializeField] private Text coinCostText;
    public UpgradeInventoryTrigger upgradeInventoryTrigger;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinCost(upgradeInventoryTrigger.CurrentCostUpInventory);
        upgradeInventoryTrigger.CoinCostChangedUpInventory += UpdateCoinCost;
    }

    public void UpdateCoinCost(int amountCost)
    {
        coinCostText.text = amountCost.ToString();
    }

    void OnDestroy()
    {

        if (upgradeInventoryTrigger != null)
        {
            upgradeInventoryTrigger.CoinCostChangedUpInventory -= UpdateCoinCost;
        }
    }

}
