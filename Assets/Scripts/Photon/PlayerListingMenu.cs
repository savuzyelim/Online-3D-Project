using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI readyUpText;
    [SerializeField] private PlayerListingPrefab playerListing;
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI _readyUpText;
    private bool _ready = false;

    private List<PlayerListingPrefab> _listings = new List<PlayerListingPrefab>();

    private RoomsCanvases _roomsCanvases;

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
        //GetCurrentRoomPlayers();
        GetCurrentPlayers();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
        {
            Destroy(_listings[i].gameObject);
        }

        _listings.Clear();
    }

    private void GetCurrentPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
        // Kendinizi listeye eklemek için
        AddPlayerListing(PhotonNetwork.LocalPlayer);
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (state)
        {
            if (_ready)
            {
                _readyUpText.text = "Ready";
            }
            else
            {
                _readyUpText.text = "Not Ready";
            }
        }

    }
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    private void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListingPrefab listing = Instantiate(playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomsCanvases.CurrentRoomCanvas.leaveRoomMenu.OnClick_LeaveRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void Onclick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            for(int i = 0; i < _listings.Count; i++)
            {
                if(_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listings[i].ready)
                    {
                        return;
                    }
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void Onclick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);

        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player,bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].ready = ready;
        }
    }
}
