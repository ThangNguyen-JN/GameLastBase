using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockObjUI : MonoBehaviour
{
    public Text resourceUnlockObjUI;
    public UnlockTrigger unlockTrigger;

    public void Start()
    {
        ChangeUIUnlock(unlockTrigger.CurrentResource);
        unlockTrigger.ChangeResourceUnlock += ChangeUIUnlock;
    }

    public void ChangeUIUnlock(int value)
    {
        resourceUnlockObjUI.text = value.ToString();
    }

    public void OnDestroy()
    {
        unlockTrigger.ChangeResourceUnlock -= ChangeUIUnlock;
    }

}
