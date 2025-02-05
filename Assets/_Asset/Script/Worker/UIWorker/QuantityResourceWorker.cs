using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantityResourceWorker : MonoBehaviour
{
    public event Action<int, int> OnQuantityResourceChange;
    [SerializeField]private int quantityResource;
    public int maxQuantity = 7;
    public int QuantityResource
    {
        get { return quantityResource; }
        set
        {
            quantityResource = Mathf.Clamp(value, 0 , maxQuantity);
            OnQuantityResourceChange?.Invoke(quantityResource, maxQuantity);
        }
    }

    public void AddResourceWorker(int value)
    {
        QuantityResource += value;
    }

    public void MinusResourceWorker(int value)
    {
        QuantityResource -= value;
    }    
}

