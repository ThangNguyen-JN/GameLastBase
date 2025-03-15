using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleLeaderboard : MonoBehaviour
{
    public static GoogleLeaderboard Instance;

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

    public void PostScoreToLeaderboard(long score)
    {
        if (!GoogleLogin.Instance.IsLoggedIn())
        {
            Debug.LogError("nguoi choi chua dang nhap googleplay!");
            return;
        }

        string leaderboardID = "CgkIyqnG1LcDEAIQAg"; 
        Social.ReportScore(score, leaderboardID, (success) =>
        {
            if (success)
                Debug.Log("gui diem thanh cong!" + score);
            else
                Debug.LogError("gui diem that bai");
        });
    }

    public void ShowLeaderboardUI()
    {
        if (!GoogleLogin.Instance.IsLoggedIn())
        {
            Debug.LogError("nguoi choi chua dang nhap thanh cong!");
            return;
        }

        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyqnG1LcDEAIQAg");
    }
}
