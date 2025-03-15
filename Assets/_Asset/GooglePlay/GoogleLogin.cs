using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleLogin : MonoBehaviour
{
    public static GoogleLogin Instance;
    public string Token { get; private set; }
    public string Error { get; private set; }

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
            return;
        }

        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
            }
        });
    }

    public bool IsLoggedIn()
    {
        return PlayGamesPlatform.Instance.IsAuthenticated();
    }
}
