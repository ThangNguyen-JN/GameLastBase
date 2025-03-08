using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestMain;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public CoinManager coinManager;
    public QuestSystemData questSystemData;
    public Transform questUIContainer;
    public GameObject questUIPrefab;
    public GameObject completeMissionUIPrefab;
    private List<QuestUIItem> questUIItems = new List<QuestUIItem>();
    public int currentQuestIndex = 0;
    public List<UnlockObjectData> unlockObjectList = new List<UnlockObjectData>();
    private Dictionary<string, GameObject> unlockObjectMap = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    public void Start()
    {
        foreach (var data in unlockObjectList)
        {
            if (data.gameObject != null)
            {
                unlockObjectMap[data.objectName] = data.gameObject;
                data.gameObject.SetActive(false); // Ẩn tất cả lúc đầu
            }
        }

        LoadQuestProgress();
        InitializeQuestsUI();
        UpdateQuestStatus();
    }

    void InitializeQuestsUI()
    {
        questUIItems.Clear();
        if (currentQuestIndex < questSystemData.questMain.Count)
        {
            GameObject questUIObj = Instantiate(questUIPrefab, questUIContainer);
            QuestUIItem questUIItem = questUIObj.GetComponent<QuestUIItem>();

            if (questUIItem != null)
            {
                questUIItem.InitializeUI(questSystemData.questMain[currentQuestIndex]);
                questUIItems.Add(questUIItem);

            }
        }
    }

    public void CompleteQuest()
    {
        if (currentQuestIndex < questSystemData.questMain.Count)
        {
            var currentQuest = questSystemData.questMain[currentQuestIndex];
            currentQuest.isCompleted = true;
            currentQuest.CheckCompletion();
            Debug.Log($"[Quest] {currentQuest.questID} completed!");

            foreach (var uiItem in questUIItems)
            {
                Destroy(uiItem.gameObject);
            }
            questUIItems.Clear();

            if (currentQuest.questType == QuestMain.QuestType.Unlock)
            {
                PlayerPrefs.SetInt($"Unlock_{currentQuest.questName}", 1);
                PlayerPrefs.Save();
            }

            ShowCompleteMissionUI(currentQuest);
         
        }
    }

    private void ShowCompleteMissionUI(QuestMain completedQuest)
    {
        Debug.Log($"[QuestManager] Showing Complete Mission UI for: {completedQuest.questTitle}");
        GameObject uiObj = Instantiate(completeMissionUIPrefab, questUIContainer);
        QuestCompleteUI completeMissionUI = uiObj.GetComponent<QuestCompleteUI>();

        if (completeMissionUI != null)
        {
            completeMissionUI.Initialize(completedQuest, this);
        }
        else
        {
            Debug.LogError("[QuestManager] Missing CompleteMissionUI component!");
        }
    }

    public void CompleteMissionReward()
    {
        if (currentQuestIndex < questSystemData.questMain.Count)
        {
            var currentQuest = questSystemData.questMain[currentQuestIndex];

            if (currentQuest.isCompleted)
            {
                coinManager.AddCoin(currentQuest.coinRewards);

                currentQuestIndex++;
                SaveQuestProgress();
                UpdateQuestStatus();
            }
        }
    }

    private void UpdateQuestStatus()
    {
        for (int i = 0; i < questSystemData.questMain.Count; i++)
        {
            if (i < currentQuestIndex)
            {
                questSystemData.questMain[i].isCompleted = true;
                questSystemData.questMain[i].isUnlock = false;
            }
            else if (i == currentQuestIndex)
            {
                questSystemData.questMain[i].isCompleted = false;
                questSystemData.questMain[i].isUnlock = true;  // Mở khóa nhiệm vụ hiện tại
            }
            else
            {
                questSystemData.questMain[i].isCompleted = false;
                questSystemData.questMain[i].isUnlock = false;
            }
        }

        // Cập nhật UI
        foreach (var uiItem in questUIItems)
        {
            Destroy(uiItem.gameObject);
        }
        questUIItems.Clear();

        // Khởi tạo UI mới cho nhiệm vụ hiện tại
        InitializeQuestsUI();
    }

    public void UpdateQuestProgress(string questTarget, int amount)
    {
        for (int i = 0; i < questSystemData.questMain.Count; i++)
        {
            QuestMain quest = questSystemData.questMain[i];

            // Nếu là nhiệm vụ thu thập (Collect)
            if (quest.questType == QuestMain.QuestType.Collect && quest.questName == questTarget)
            {
                quest.currentAmount += amount;
                quest.CheckCompletion();
            }

            // Nếu là nhiệm vụ nâng cấp (Upgrade)
            if (quest.questType == QuestMain.QuestType.Upgrade && quest.questName == questTarget)
            {
                quest.currentAmount = amount; // Gán trực tiếp cấp độ
                quest.CheckCompletion();
            }

            if (quest.questType == QuestMain.QuestType.Unlock && quest.questName == questTarget)
            {
                quest.currentAmount = amount;
                quest.CheckCompletion();
            }    

            // Nếu nhiệm vụ đã hoàn thành và đến đúng lượt, gọi CompleteQuest()
            if (quest.isCompleted && i == currentQuestIndex)
            {
                CompleteQuest();
            }
        }
    }

    private void SaveQuestProgress()
    {
        PlayerPrefs.SetInt("CurrentQuestIndex", currentQuestIndex);
        PlayerPrefs.Save();
    }

    private void LoadQuestProgress()
    {
        if (PlayerPrefs.HasKey("CurrentQuestIndex"))
        {
            currentQuestIndex = PlayerPrefs.GetInt("CurrentQuestIndex");
        }

        for (int i = 0; i < questSystemData.questMain.Count; i++)
        {
            QuestMain quest = questSystemData.questMain[i];

            if (quest.questType == QuestMain.QuestType.Unlock)
            {
                if (PlayerPrefs.GetInt($"Unlock_{quest.questName}", 0) == 1)
                {
                    quest.isCompleted = true;
                    quest.currentAmount = quest.targetAmount;

                    // Nếu nhiệm vụ này chưa được đánh dấu hoàn thành trước đó, cập nhật ngay
                    if (i == currentQuestIndex)
                    {
                        CompleteQuest();
                    }
                }
            }
        }
    }

}
