using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField] private byte maxPlayersPerRoom = 4;

    [SerializeField] private GameObject roomManagerPrefab;

    [SerializeField] private TMP_InputField roomInputfield;

    [SerializeField] private GameObject LobbyPanel;
    [SerializeField] private GameObject ErrorPanel;
    [SerializeField] private RoomItem roomItemPrefab;

    List<RoomItem> roomItemsList = new List<RoomItem>();
    [SerializeField] private Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;
    private string existingName;

    public static byte MaxPlayersPerRoom;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        MaxPlayersPerRoom = maxPlayersPerRoom;
    }

    public void OnClickCreate()
    {
        if (roomInputfield.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputfield.text, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });
        }
    }

    public override void OnJoinedRoom()
    {
        ////Waiting Room
        if (PhotonNetwork.CurrentRoom.PlayerCount < maxPlayersPerRoom)
        {
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();
        foreach(RoomInfo room in roomList)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void joinRoom(string _roomname)
    {
        PhotonNetwork.JoinRoom(_roomname);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
