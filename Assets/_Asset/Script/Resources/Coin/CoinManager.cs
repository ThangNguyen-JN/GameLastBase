using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        LoadCoin();
    }

    private void Update()
    {
       
    }

    

    public void AddCoin(int amount)
    {
        Coin += amount;
        SaveCoin();
    }

    public void SpendCoin(int amount)
    {
        Coin -= amount;
        SaveCoin();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("CurrentCoin", Coin);
        PlayerPrefs.Save();
    }    

    public void LoadCoin()
    {
        Coin = PlayerPrefs.GetInt("CurrentCoin", 0);
    }

    public void OnApplicationQuit()
    {
        SaveCoin();
    }

    
    
}
