using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;
using System;

public class PhotonChatController : MonoBehaviour, IChatClientListener
{
    [SerializeField] private string nickName;
    private ChatClient chatClient;

    private void Awake()
    {
        nickName = PlayerPrefs.GetString("USERNAME");
    }

    private void Start()
    {
        chatClient = new ChatClient(this);
        connectToPhotonChat();
    }

    // Update is called once per frame
    private void Update()
    {
        chatClient.Service();
    }

    private void connectToPhotonChat()
    {
        Debug.Log("Connecting to Photon Chat");
        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(nickName);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(nickName));
    }

    public void SendDrirectMessage(string recipient, string message)
    {
        chatClient.SendPrivateMessage(recipient, message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log($"{level}: {message}");
    }

    public void OnDisconnected()
    {
        Debug.Log("You Have disconnected from chat");
    }

    public void OnConnected()
    {
        Debug.Log("You Have Connected to chat");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"Chat state changed to: {state}");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            Debug.Log($"{senders[i]}: {messages[i]}");
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"Private message from {sender}: {message}");
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log($"Subscribed to channel: {channels[i]}, result: {results[i]}");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log($"Unsubscribed from channel: {channels[i]}");
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"Status update from {user}: {status}, got message: {gotMessage}, message: {message}");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log($"{user} subscribed to channel: {channel}");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"{user} unsubscribed from channel: {channel}");
    }
}
