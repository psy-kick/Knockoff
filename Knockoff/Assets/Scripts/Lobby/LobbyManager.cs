using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    public byte maxPlayersPerRoom = 4;

    public TMP_InputField roomInputfield;
    
    public GameObject LobbyPanel;
    public GameObject ErrorPanel;
    public RoomItem roomItemPrefab;

    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;
    private string existingName;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
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
        ////Game Room
        else
        {
            PhotonNetwork.LoadLevel("Room for 4");
        }

        //if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        //{
        //    PhotonNetwork.LoadLevel("Room for 1");
        //}
        ////Game Room
        //else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        //{
        //    PhotonNetwork.LoadLevel("Room for 4");
        //}
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
