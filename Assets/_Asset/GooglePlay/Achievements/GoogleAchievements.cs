using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAchievements : MonoBehaviour
{
    public static GoogleAchievements Instance;

    void Awake()
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

    //Mo giao dien
    public void ShowAchievementsUI()
    {
        if (!GoogleLogin.Instance.IsLoggedIn())
        {
            Debug.LogError("Người chơi chưa đăng nhập Google Play Games!");
            return;
        }

        Social.ShowAchievementsUI();
    }

    //Mo khoa
    public void UnlockAchievement(string achievementID)
    {
        if (!GoogleLogin.Instance.IsLoggedIn())
        {
            Debug.LogError("Người chơi chưa đăng nhập Google Play Games!");
            return;
        }

        Social.ReportProgress(achievementID, 100.0f, (success) =>
        {
            if (success)
                Debug.Log("Mở khóa Achievement thành công: " + achievementID);
            else
                Debug.LogError("Mở khóa Achievement thất bại!");
        });
    }
    
    // Cập nhật tiến trình một Achievement dạng Incremental (có nhiều cấp độ).
    
    public void IncrementAchievement(string achievementID, int stepsToIncrement)
    {
        if (!GoogleLogin.Instance.IsLoggedIn())
        {
            Debug.LogError("Người chơi chưa đăng nhập Google Play Games!");
            return;
        }

        PlayGamesPlatform.Instance.IncrementAchievement(achievementID, stepsToIncrement, (success) =>
        {
            if (success)
                Debug.Log("Cập nhật tiến trình Achievement thành công: " + achievementID);
            else
                Debug.LogError("Cập nhật tiến trình Achievement thất bại!");
        });
    }
}
