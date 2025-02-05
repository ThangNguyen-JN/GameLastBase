using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiQuantityResourceWorker : MonoBehaviour
{
    public Text textQuantityResourceWorker;
    public QuantityResourceWorker quanityResourceWorker;
    // Start is called before the first frame update
    void Start()
    {
        quanityResourceWorker.OnQuantityResourceChange += ChangeTextQuantityResourceWorker;
        ChangeTextQuantityResourceWorker(quanityResourceWorker.QuantityResource, quanityResourceWorker.maxQuantity);
    }


    public void ChangeTextQuantityResourceWorker(int quantity, int maxQuantity)
    {
        textQuantityResourceWorker.text = $"{quantity} / {maxQuantity}";
    }
    private void OnDisable()
    {
        quanityResourceWorker.OnQuantityResourceChange -= ChangeTextQuantityResourceWorker;
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

}
