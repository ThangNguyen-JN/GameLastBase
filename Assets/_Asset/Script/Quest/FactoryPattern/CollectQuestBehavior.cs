using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuestBehavior : IQuestBehavior
{
    public void StartQuest(QuestMain quest)
    {
        Debug.Log($"Starting Collect Quest: {quest.questID}");
    }

    public void UpdateProgress(QuestMain quest, int progress)
    {
        quest.currentAmount = Mathf.Clamp(quest.currentAmount + progress, 0, quest.targetAmount);
        Debug.Log($"Collected {progress} items. Current progress: {quest.currentAmount}/{quest.targetAmount}");
    }

    public bool CheckCompletion(QuestMain quest)
    {
        return quest.currentAmount >= quest.targetAmount;
    }
}
