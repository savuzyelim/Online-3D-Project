using System;
using TMPro;
using Photon.Realtime;
using UnityEngine;

public class UIFriend : MonoBehaviour
{
    [SerializeField] private TMP_Text friendNameText;
    [SerializeField] private FriendInfo friend;

    public static Action<string> OnRemoveFriend = delegate { };

    public void OnServerInitialized(FriendInfo friend)
    {
        this.friend = friend;
        friendNameText.SetText(this.friend.UserId);
    }

    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friend.UserId);
    }
}
