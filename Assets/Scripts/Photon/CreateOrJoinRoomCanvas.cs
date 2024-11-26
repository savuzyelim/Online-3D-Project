using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{

    [SerializeField] private CreateRoom _createRoom;
    private RoomsCanvases _roomsCanvases;

    [SerializeField] private RoomListings _roomListings;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _createRoom.FirstInitialize(canvases);
        _roomListings.FirstInitialize(canvases);

    }
}
