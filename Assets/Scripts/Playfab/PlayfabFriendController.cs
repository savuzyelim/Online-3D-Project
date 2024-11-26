using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;

public class PlayfabFriendController : MonoBehaviour
{

    public static Action<List<FriendInfo>> OnFriendListUpdated = delegate { };
    private List<FriendInfo> friends;
    private void Awake()
    {
        friends = new List<FriendInfo>();
        PhotonConnector.GetPhotonFriends += HandleGetFriends;
        UIAddFriend.OnAddFriend += HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend += HandleRemoveFriend;
    }

    private void HandleGetFriends()
    {
        GetPlayFabFriends();
    }

    private void OnDestroy()
    {
        UIAddFriend.OnAddFriend -= HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend -= HandleRemoveFriend;
    }

    private void HandleAddPlayfabFriend(string name)
    {
        var request = new AddFriendRequest { FriendTitleDisplayName = name };
        PlayFabClientAPI.AddFriend(request, OnFriendAddSuccess, OnFailure);
    }
    private void HandleRemoveFriend(string name)
    {
        string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
        var request = new RemoveFriendRequest { FriendPlayFabId = id };
        PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, OnFailure);
    }

    private void OnFriendRemoveSuccess(RemoveFriendResult result)
    {
        GetPlayFabFriends();
    }

    private void OnFriendAddSuccess(AddFriendResult result)
    {
        GetPlayFabFriends();
    }
    private void GetPlayFabFriends()
    {
        var request = new GetFriendsListRequest
        {
            ExternalPlatformFriends = PlayFab.ClientModels.ExternalFriendSources.None,
            XboxToken = null
        };

        PlayFabClientAPI.GetFriendsList(request, OnFriendsListSuccess, OnFailure);
    }

    private void OnFriendsListSuccess(GetFriendsListResult result)
    {
        friends = result.Friends;
        OnFriendListUpdated?.Invoke(result.Friends);
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"Error occured when adding friend {error.GenerateErrorReport()}");
    }
    private void Start()
    {
        
    }
}
