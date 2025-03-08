using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : MonoBehaviour
{
    public GameObject panelSetting;

    public void OpenSettingButton()
    {
        panelSetting.SetActive(true);
        Time.timeScale = 0;
    }
}
