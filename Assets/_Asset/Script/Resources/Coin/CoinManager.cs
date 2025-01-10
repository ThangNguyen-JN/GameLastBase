using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private static CoinManager instance;
    public static CoinManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CoinManager>();
                if (instance == null)
                {
                    GameObject coinManagerObject = new GameObject("CoinManager");
                    instance = coinManagerObject.AddComponent<CoinManager>();
                    DontDestroyOnLoad(coinManagerObject);

                }
            }
            return instance;
        }
    }
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

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadCoin();
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
