using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClose : MonoBehaviour
{
    public GameObject panelSetting;

    public void CloseSettingButton()
    {
        panelSetting.SetActive(false);
        Time.timeScale = 1;
    }
}
