using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textCoin;
    public CoinManager coinManager;

    void Start()
    {
        if (coinManager != null)
        {
            coinManager.CoinChangeUpdate += UpdateCoin;
            UpdateCoin(coinManager.Coin);
        }
    }

    public void UpdateCoin(int coinAmount)
    {
        textCoin.text = coinAmount.ToString();
    }

    private void OnDestroy()
    {
        coinManager.CoinChangeUpdate -= UpdateCoin;
    }


}
