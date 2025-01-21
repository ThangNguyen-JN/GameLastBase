using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TurretUpgradeLever
{
    public string leverTurret;
    public string nameResource;
    public Sprite imageResource;
    public int resourceQuantily;

    public void SubtractAmountResource(int value)
    {
        resourceQuantily = Mathf.Clamp(resourceQuantily - value, 0, resourceQuantily);

    }
}
