using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageQuantityUI : MonoBehaviour
{
    public StorageQuantity storageQuantity;
    public Text textResource;
    public Image imageResource;

    // Start is called before the first frame update
    void Start()
    {
        storageQuantity.ChangedCurrentResourceStorage += ChangeCurrentResourceStorage;
        ChangeCurrentResourceStorage(storageQuantity.CurrentResource, storageQuantity.maxResource);
        imageResource.sprite = storageQuantity.imageStorage;
    }

    public void ChangeCurrentResourceStorage(int current, int max)
    {
        textResource.text = current.ToString() + " /" + max.ToString();
    }

    public void OnDisable()
    {
        storageQuantity.ChangedCurrentResourceStorage -= ChangeCurrentResourceStorage;
    }
}
