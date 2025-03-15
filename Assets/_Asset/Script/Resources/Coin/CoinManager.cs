using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    
    public event Action<int> CoinChangeUpdate;
    private int currentCoin;

    public int Coin
    {
        get { return currentCoin; }
        set 
        { 
            currentCoin = Mathf.Clamp(value, 0, 999);
            CoinChangeUpdate?.Invoke(currentCoin);
        }
    }

    private int totalCoinsCollected;

    public int TotalCoinsCollected
    {
        get { return totalCoinsCollected; }
        set
        {
            totalCoinsCollected = value;
            PlayerPrefs.SetInt("TotalCoinsCollected", totalCoinsCollected); // Luu lai tong so coin
            PlayerPrefs.Save();
        }
    }


    private void Start()
    {
        LoadCoin();
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
        TotalCoinsCollected += amount;
        SaveCoin();
        //GoogleLeaderboard.Instance.PostScoreToLeaderboard(TotalCoinsCollected);
    }

    public void SpendCoin(int amount)
    {
        Coin -= amount;
        SaveCoin();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("CurrentCoin", Coin);
        PlayerPrefs.SetInt("TotalCoinsCollected", TotalCoinsCollected);
        PlayerPrefs.Save();
    }    

    public void LoadCoin()
    {
        Coin = PlayerPrefs.GetInt("CurrentCoin", 0);
        TotalCoinsCollected = PlayerPrefs.GetInt("TotalCoinsCollected", 0);
    }

    public void OnApplicationQuit()
    {
        SaveCoin();
    }

    
    
}
