using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListingPrefab roomListing;
    [SerializeField] private Transform content;

    private List<RoomListingPrefab> _listings = new List<RoomListingPrefab>();


    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    public override void OnJoinedRoom()
    {
        _roomsCanvases.CurrentRoomCanvas.Show();
        content.DestroyChildren();
        _listings.Clear();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListingPrefab listing = Instantiate(roomListing, content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }

                }
                else
                {
                    //modify
                }
            }

        }
    }
}
