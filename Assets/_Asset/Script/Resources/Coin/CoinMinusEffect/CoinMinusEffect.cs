using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMinusEffect : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform upgradeTable;
    public Transform playerPositon;

    public void SpawnCoinEffect()
    {
        GameObject coin = Instantiate(coinPrefab, playerPositon.position, Quaternion.identity);
        CoinEffect coinEffect = coin.GetComponent<CoinEffect>();
        if (coinEffect != null)
        {
            coinEffect.SetTarget(upgradeTable.position);
        }
    }
}
