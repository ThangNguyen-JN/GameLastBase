using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketUI : MonoBehaviour
{
    [SerializeField] private Text ticketText;
    public TicketManager ticketManager;
    // Start is called before the first frame update
    void Start()
    {
        ticketManager.TicketChangeUpdate += UpdateTicket;
        UpdateTicket(ticketManager.Ticket);
    }

    
    private void UpdateTicket(int amountTicket)
    {
        ticketText.text = ticketManager.Ticket.ToString();
    }

    private void OnDestroy()
    {
        ticketManager.TicketChangeUpdate -= UpdateTicket;
    }

}
