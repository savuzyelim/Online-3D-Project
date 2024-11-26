using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    
    public static Action GetPhotonFriends = delegate { };

    #region Unity Method
    private void Start()
    {
        string nickname = PlayerPrefs.GetString("USERNAME");
        ConnectToPhoton(nickname);
    }

    
    #endregion

    #region Private Methods
    private void ConnectToPhoton(string nickName)
    {
        Debug.Log($"Connected to Photon with nickname: {nickName}");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void CreatePhotonRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("You have connected to the Photon Master Server");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to the Photon Lobby");
        Debug.Log($"Current nickname: {PhotonNetwork.NickName}");

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            string nickname = PlayerPrefs.GetString("USERNAME");
            PhotonNetwork.NickName = nickname;
            Debug.Log($"Nickname set from PlayerPrefs: {nickname}");
        }

        GetPhotonFriends?.Invoke();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a Photon room named {PhotonNetwork.CurrentRoom.Name}");
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"You have joined a Photon room named {PhotonNetwork.CurrentRoom.Name}");
        base.OnJoinedRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a Photon room");
        base.OnLeftRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"You have failed to join a Photon room: {message}");
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Another player has joined the room: {newPlayer.UserId}");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player has left the room: {otherPlayer.UserId}");
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"New Master Client is {newMasterClient.UserId}");
        base.OnMasterClientSwitched(newMasterClient);
    }

    
    #endregion
}
