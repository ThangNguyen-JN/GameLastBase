using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        ResourceDatabase.Instance.LoadResource();
    }

    private void OnApplicationQuit()
    {
        ResourceDatabase.Instance.SaveResource();
    }
}
