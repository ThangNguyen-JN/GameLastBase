using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    [SerializeField]private Text textCoin;

    void Start()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.CoinChangeUpdate += UpdateCoin;
            UpdateCoin(CoinManager.Instance.Coin);
        }
    }

    public void UpdateCoin(int coinAmount)
    {
        textCoin.text = coinAmount.ToString();
    }

    private void OnDestroy()
    {
        CoinManager.Instance.CoinChangeUpdate -= UpdateCoin;
    }


}
