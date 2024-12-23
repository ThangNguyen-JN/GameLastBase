using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    [SerializeField]private Text textCoin;
    public CoinManager coinManager;

    void Start()
    {
        coinManager.CoinChangeUpdate += UpdateCoin;
        UpdateCoin(coinManager.Coin);
    }

    public void UpdateCoin(int coinAmount)
    {
        textCoin.text = coinManager.Coin.ToString();
    }

    private void OnDestroy()
    {
        coinManager.CoinChangeUpdate -= UpdateCoin;
    }


}
