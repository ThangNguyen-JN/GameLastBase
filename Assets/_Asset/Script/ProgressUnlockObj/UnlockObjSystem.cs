using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockObjSystem : MonoBehaviour
{
    public List<GameObject> objUnlock = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        HiddenObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HiddenObj()
    {
        foreach (GameObject obj in objUnlock) 
        { 
            obj.SetActive(false);
        }
    }
    
    public void ShowObj()
    {
        foreach (GameObject obj in objUnlock)
        {
            obj.SetActive(true);
        }
    }
}
