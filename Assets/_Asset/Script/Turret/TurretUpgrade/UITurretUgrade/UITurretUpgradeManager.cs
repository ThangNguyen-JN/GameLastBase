using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeLeverDatabase turretUpLvData;
    
    public Image resourceUpImage;
    public Text resourceUpText;

    private int currentLeverIndex = 0;

    public void UpdateUI()
    {
        
    }

    public void NextLevel()
    {
        if (currentLeverIndex < turretUpLvData.turretUpLever.Count - 1)
        {
            currentLeverIndex++;
            UpdateUI();
        }
        else
        {
            Debug.Log("Already at max level.");
        }
    }
}
