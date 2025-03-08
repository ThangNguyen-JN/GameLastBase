using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoogle : MonoBehaviour
{
    public GameObject panelGoogle;
    public void OpenGoogle()
    {
        panelGoogle.SetActive(true);
        Time.timeScale = 0;
    }
}
