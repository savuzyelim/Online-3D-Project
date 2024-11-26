using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] private string username;
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "4FCDA";
        }
    }

    public void SetUsername(string name)
    {
        username = name;
        PlayerPrefs.SetString("USERNAME", username);
    }
    public void Login()
    {
        if (!IsValidUsername()) return;

        LoginWithCustomId();
    }

    private bool IsValidUsername()
    {
        bool isValid = false;
        if (username.Length >= 3 && username.Length <= 24)
        {
            isValid = true;
        }
        return isValid;
    }
    private void LoginWithCustomId()
    {
        Debug.Log($"Login to playfab as {username}");
        var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
    }

    public void UpdateDisplayName(string displayName)
    {
        Debug.Log($"Displaying name {displayName}");
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the displayname of the playfab acc");
        SceneController.LoadScene("MainMenu");
    }
    private void OnLoginCustomIdSuccess(LoginResult result)
    {
        Debug.Log($"You have logged in playfab using custom id {username}");
        UpdateDisplayName(username);
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"there was an issue with your request {error.GenerateErrorReport()}");
    }

}
