using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ResourceDatabase resourceDatabase;

    private void Start()
    {
        resourceDatabase.LoadResource();
    }

    private void OnApplicationQuit()
    {
        resourceDatabase.SaveResource();
    }
}
