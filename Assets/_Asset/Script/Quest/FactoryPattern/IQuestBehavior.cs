using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestBehavior
{
    void StartQuest(QuestMain quest);
    void UpdateProgress(QuestMain quest, int progress);
    bool CheckCompletion(QuestMain quest);
}
