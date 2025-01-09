using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIInitializer : MonoBehaviour
{
    public GameObject resourceUIPrefab; //prefab chua UI
    public Transform uiParent; //Parent de chua UI

    // Start is called before the first frame update
    void Start()
    {
        InitializeResouceUI(); // goi ham khoi tao ui
    }

    private void InitializeResouceUI()
    {
        ResourceDatabase resourceDatabase = ResourceDatabase.Instance;
        //lap qua cac tai nguyen trong ResourceDatabase
        foreach (Resource resource in resourceDatabase.resources)
        {
            //kiem tra neu tai nguyen duoc mo
            if (resource.unlock == true)
            {
                //tao phan tu ui moi tu prefab voi uiParent la cha
                GameObject uiElement = Instantiate(resourceUIPrefab, uiParent);

                //lay component ResourceUiManager tu phan tu Ui vua tao
                ResourceUIManager uiManager = uiElement.GetComponent<ResourceUIManager>();
                if (uiManager != null)
                {
                    //thiet lap cac thong tin
                    uiManager.resourceName = resource.resourceName; // ten tai nguyen
                    //khoi tao giao dien tai nguyen
                    uiManager.InitializeUI();
                }

            }

            
        }
    }
}
