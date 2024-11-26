using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PhotonFriendController : MonoBehaviourPunCallbacks
{

    public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate { };
    private void Awake()
    {
        PlayfabFriendController.OnFriendListUpdated += HandleFriendsUpdated;
    }
    private void OnDestroy()
    {
        PlayfabFriendController.OnFriendListUpdated -= HandleFriendsUpdated;
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        if (friends.Count != 0)
        {
            string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
            OnDisplayFriends?.Invoke(friendList);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        OnDisplayFriends?.Invoke(friendList);
    }
}
