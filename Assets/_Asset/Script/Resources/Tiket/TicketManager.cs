using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public event Action<int> TicketChangeUpdate;
    private int currentTicket;

    public int Ticket
    {
        get { return currentTicket; }
        set
        {
            currentTicket = Mathf.Clamp(value, 0, 99);
            TicketChangeUpdate?.Invoke(currentTicket);
        }
    }

    private void Start()
    {
        LoadTicket();
    }

    private void Update()
    {

    }



    public void AddTicket(int amount)
    {
        Ticket += amount;
        SaveTicket();
    }

    public void SpendTicket(int amount)
    {
        Ticket -= amount;
        SaveTicket();
    }

    public void SaveTicket()
    {
        PlayerPrefs.SetInt("CurrentTicket", Ticket);
        PlayerPrefs.Save();
    }

    public void LoadTicket()
    {
        Ticket = PlayerPrefs.GetInt("CurrentTicket", 0);
    }

    public void OnApplicationQuit()
    {
        SaveTicket();
    }

}
