using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class CreateRoom : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI createdRoomName;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    public void CreatePhotonRoom()
    {

        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions ro = new RoomOptions();
        ro.BroadcastPropsChangeToAll = true;
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(createdRoomName.text, ro, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a Photon room named {PhotonNetwork.CurrentRoom.Name}");
        base.OnCreatedRoom();
        _roomsCanvases.CurrentRoomCanvas.Show();

    }
}
