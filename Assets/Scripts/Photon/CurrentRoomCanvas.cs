using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomsCanvases _roomsCanvases;
    [SerializeField] private PlayerListingMenu _playerListingMenu;
    [SerializeField] private LeaveRoomMenu _leaveRoomMenu;
    public LeaveRoomMenu leaveRoomMenu { get { return _leaveRoomMenu; } }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _playerListingMenu.FirstInitialize(canvases);
        _leaveRoomMenu.FirstInitialize(canvases);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
