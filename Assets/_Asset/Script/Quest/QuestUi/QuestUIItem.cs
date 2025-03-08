using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIItem : MonoBehaviour
{
    
    public Image questIconUi;
    public Text questTitleUi;
    public Image bgProgressUi;
    public Image fillProgressUi;
    public Text progressTextUi;

    public QuestMain quest;

    public void InitializeUI(QuestMain questMain)
    {
        if (questMain == null) return;

        if (quest != null)
        {
            quest.OnQuestUpdated -= UpdateUI;
        }

        quest = questMain;
        questIconUi.sprite = quest.questIcon;
        questTitleUi.text = quest.questTitle;

        quest.OnQuestUpdated += UpdateUI;

        UpdateUI(quest);
    }



    private void UpdateUI(QuestMain quest)
    {
        if (this.quest == quest)
        {
            UpdateProgressText();
            UpdateProgressFillUI();
        }
    }

    public void UpdateProgressText()
    {
        if (quest == null) return;
        progressTextUi.text = $"{quest.currentAmount}/{quest.targetAmount}";
    }

    public void UpdateProgressFillUI()
    {
        if (quest == null) return;

        float progress = (float)quest.currentAmount / quest.targetAmount;
        fillProgressUi.fillAmount = Mathf.Clamp01(progress);
    }

    private void OnDestroy()
    {
        if (quest != null)
        {
            quest.OnQuestUpdated -= UpdateUI; 
        }
    }
}
