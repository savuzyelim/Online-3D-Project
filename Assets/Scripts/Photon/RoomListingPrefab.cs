using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomListingPrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;


    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roominfo)
    {
        RoomInfo = roominfo;
        _text.text = roominfo.MaxPlayers + "/" + roominfo.Name;
    }

    public void Onclick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
