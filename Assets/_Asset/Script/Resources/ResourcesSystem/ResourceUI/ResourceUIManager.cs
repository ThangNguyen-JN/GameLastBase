using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIManager : MonoBehaviour
{
    //ten tai nguyen lien ket voi UI
    public string resourceName;
    //thanh phan giao dien UI
    public Image iconUI;
    public Text amountTextUI;
    
    public ResourceDatabase resourceDatabase;

    private void Start()
    {
        InitializeUI(); // khoi tao UI
        ResourceDatabase.Instance.OnResourceChanged += UpdateUI; //Dang ky su kien
    }

    private void OnDestroy()
    {
        ResourceDatabase.Instance.OnResourceChanged -= UpdateUI; //Huy su kien
    }

    //thuc hien khoi tao UI
    public void InitializeUI()
    {
        //lay tai nguyen tu resourceDatabase dua theo ten
        Resource resource = ResourceDatabase.Instance.GetResource(resourceName);
        if (resource != null) //tim thay tai nguyen va cap nhat
        {
            if (iconUI != null)
            {
                iconUI.sprite = resource.iconPath;
            }
            UpdateAmountText(resource);
        }
    }
    private void UpdateUI(Resource resource)
    {
        //chi cap nhat UI neu tai nguyen thay doi la tai nguyen hien tai
        if (resource.resourceName == resourceName)
        {
            UpdateAmountText(resource);
        }
    }

    //cap nhat van ban hien thi so luong tai nguyen
    private void UpdateAmountText(Resource resource)
    {
        if (amountTextUI != null)
        {
            amountTextUI.text = $"{resource.amount} / {resource.maxAmount}";
        }
    }
}
