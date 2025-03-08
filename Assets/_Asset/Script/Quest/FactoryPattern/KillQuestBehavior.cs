using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuestBehavior : IQuestBehavior
{
    public void StartQuest(QuestMain quest)
    {
        Debug.Log($"Starting Kill Quest: {quest.questID}");
    }

    public void UpdateProgress(QuestMain quest, int progress)
    {
        quest.currentAmount = Mathf.Clamp(quest.currentAmount + progress, 0, quest.targetAmount);
        Debug.Log($"Killed {progress} enemies. Current progress: {quest.currentAmount}/{quest.targetAmount}");
    }

    public bool CheckCompletion(QuestMain quest)
    {
        return quest.currentAmount >= quest.targetAmount;
    }
}
