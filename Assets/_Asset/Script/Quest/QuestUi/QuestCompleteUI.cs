using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompleteUI : MonoBehaviour
{
    public Image missionIcon;
    public Button completeMissionButton;

    private QuestMain quest;
    private QuestManager questManager;
    

    public void Initialize (QuestMain questData, QuestManager questManager)
    {
        quest = questData;
        this.questManager = questManager;

        missionIcon.sprite = quest.questIcon;
        completeMissionButton.onClick.AddListener(OnCompleteMissionClicked);
    }

    private void OnCompleteMissionClicked()
    {
        questManager.CompleteMissionReward();
        Destroy(gameObject);
    }
}
