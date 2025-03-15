using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTrigger : MonoBehaviour
{
    public event Action<int> ChangeResourceUnlock;
    [SerializeField]private int currentResource;
    public int maxResource;
    public string resourceUnlocked;
    public string nameQuest;
    public string nameSaveUnlock;
    
    public int CurrentResource
    {
        get { return currentResource; }
        set
        {
            currentResource = Mathf.Clamp(value, 0, maxResource);
            ChangeResourceUnlock?.Invoke(currentResource);
        }

    }

    public UnlockMainRoom mainRoom;
    public GameObject unlockUi;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTakeResource"))
        {
            StartCoroutine(DeductResource());
        }
    }

    public IEnumerator DeductResource()
    {
        yield return new WaitForSeconds(1f);
        while (CurrentResource > 0)
        {
            Resource resource = ResourceDatabase.Instance.GetResource(resourceUnlocked);
            if (resource == null || resource.amount <=0 )
            {
                yield break;
            }
            ResourceDatabase.Instance.SubtractResource(resourceUnlocked, 1);
            CurrentResource--;
            yield return new WaitForSeconds(0.2f);
        }

        if (CurrentResource <= 0)
        {
            QuestManager.Instance.UpdateQuestProgress(nameQuest, 1);
            PlayerPrefs.SetInt(nameSaveUnlock, 1);
            PlayerPrefs.Save();
            mainRoom.ShowMainRoom();
            
        }

    }
}
