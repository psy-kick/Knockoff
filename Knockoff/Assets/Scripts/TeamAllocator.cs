using KnockOff.Player;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeamAllocator : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        AssignTeams();
    }
 
    // Assign players to teams
    void AssignTeams()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("TEAM ASSIGNED");
            // Get a list of all the players in the game
            List<Player> players = new List<Player>(PhotonNetwork.PlayerList);
            //players[0].joi
            // Shuffle the player list
            Shuffle(players);

            // Loop through each player and assign them to a team
            for (int i = 0; i < players.Count; i++)
            {
                int teamIndex = i % PhotonTeamsManager.Instance.GetAvailableTeams().Length;
                PhotonTeam teamName = PhotonTeamsManager.Instance.GetAvailableTeams()[teamIndex];

                // Assign the player to the team
                players[i].JoinTeam(teamName);
            }
        }
    }


    // Shuffle a list using the Fisher-Yates algorithm
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
   
}
