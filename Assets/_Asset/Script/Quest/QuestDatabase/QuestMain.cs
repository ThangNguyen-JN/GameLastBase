using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestMain
{
    public string questID;
    public string questTitle;
    public Sprite questIcon;
    public QuestType questType;
    public string questName;
    public int targetAmount;
    [SerializeField] private int _currentAmount;
    public int currentAmount
    {
        get => _currentAmount;
        set
        {
            _currentAmount = Mathf.Clamp(value, 0, targetAmount);
            OnQuestUpdated?.Invoke(this); // Goi su kien khi currentAmount thay doi
        }
    }
    public int coinRewards;
    public bool isCompleted;
    public QuestMain nextQuest;
    public bool isUnlock = false;

    public Action<QuestMain> OnQuestUpdated;
    public List<string> unlockObjectNames = new List<string>();

    public void ResetProgress()
    {
        currentAmount = 0;
    }

    public IQuestBehavior CreateBehavior()
    {
        switch (questType)
        {
            case QuestType.Collect:
                return new CollectQuestBehavior();
            case QuestType.Kill:
                return new KillQuestBehavior();
            default:
                Debug.LogError($"Quest Type {questType} is not recognized!");
                return null;
        }
    }

    public void CheckCompletion()
    {
        if (currentAmount >= targetAmount)
        {
            isCompleted = true;
            UnlockNextQuest();
        }

        if (questType == QuestType.Upgrade && currentAmount >= targetAmount)
        {
            isCompleted = true;
            UnlockNextQuest();
        }
    }

    

    private void UnlockNextQuest()
    {
        if (nextQuest != null)
        {
            nextQuest.isUnlock = true;
        }
    }

    public enum QuestType
    {
        Collect,
        Kill,
        Upgrade,
        Unlock,
    }

    [System.Serializable]
    public class UnlockObjectData
    {
        public string objectName;
        public GameObject gameObject;
    }


}
