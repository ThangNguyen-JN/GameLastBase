using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGoogle : MonoBehaviour
{
    public GameObject panelGoogle;

    public void CloseButton()
    {
        panelGoogle.SetActive(false);
        Time.timeScale = 1;
    }
    
}
