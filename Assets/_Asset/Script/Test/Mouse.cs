using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    void Update()
    {
        // Ki?m tra n?u con tr? chu?t ?ang trên màn hình
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = Input.mousePosition;

            // In v? trí chu?t
            Debug.Log("Mouse position: " + mousePosition);
        }
    }

}
