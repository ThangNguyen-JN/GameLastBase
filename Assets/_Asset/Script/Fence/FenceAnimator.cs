using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceAnimator : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play("An_Fence_Idle");
    }

    // Update is called once per frame
    void Update()
    {
         
    }

   
}
