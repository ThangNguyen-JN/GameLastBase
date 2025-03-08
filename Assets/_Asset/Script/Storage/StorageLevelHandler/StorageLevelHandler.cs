using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageLevelHandler : MonoBehaviour
{
    public StorageUpgradeManage storageUpgradeManage;
    public List<GameObject> objectsToDisable;
    // Start is called before the first frame update
    void Start()
    {
        UpdateObjectState();
    }

    public void UpdateObjectState()
    {
        if (storageUpgradeManage == null)
        {
            return;
        }

        bool shouldDisable = (storageUpgradeManage.GetCurrentLevel() == 0);

        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(!shouldDisable);
            }
        }
    }
}
