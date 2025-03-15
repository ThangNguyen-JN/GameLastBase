using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMainRoom : MonoBehaviour
{
    public string nameSaveUnlock;
    public List<GameObject> mainRoom = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(nameSaveUnlock, 0) == 1)
        {
            ShowMainRoom();
        }
        else
        {
            foreach (GameObject obj in mainRoom)
            {
                obj.SetActive(false);
            }
        }
    }

    public void ShowMainRoom()
    {
        if (mainRoom == null) return;
        foreach (GameObject obj in mainRoom)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        Destroy(gameObject);
    }
}
