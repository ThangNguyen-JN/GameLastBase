using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public GameObject effectObject;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(effectObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
