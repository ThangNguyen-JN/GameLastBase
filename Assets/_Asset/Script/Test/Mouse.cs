using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    void Update()
    {
        // Ki?m tra n?u con tr? chu?t ?ang tr�n m�n h�nh
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = Input.mousePosition;

            // In v? tr� chu?t
            Debug.Log("Mouse position: " + mousePosition);
        }
    }

}
