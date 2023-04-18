using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    LobbyManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<LobbyManager>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text= _roomName;
    }
    public void OnClickRoom()
    {
        manager.joinRoom(roomName.text);
    }
}
