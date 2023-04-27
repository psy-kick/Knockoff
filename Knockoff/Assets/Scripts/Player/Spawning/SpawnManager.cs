using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Player> players;

    [SerializeField] private SpawnPoint[] BlueTeamSpawnPts;
    [SerializeField] private SpawnPoint[] RedTeamSpawnPts;

    private void Awake()
    {
        PhotonTeamsManager.PlayerJoinedTeam += SpawnPlayers;
    }

    private void OnDestroy()
    {
        PhotonTeamsManager.PlayerJoinedTeam -= SpawnPlayers;
    }


    private void Start()
    {
        players = new List<Player>(PhotonNetwork.PlayerList);

    }

    public void SpawnPlayers(Photon.Realtime.Player p, PhotonTeam team)
    {
        if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[0])
        {
            int randomIndex = Random.Range(0, BlueTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            player.transform.position = BlueTeamSpawnPts[randomIndex].transform.position;
        }
        
        if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[1])
        {
            int randomIndex = Random.Range(0, RedTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            player.transform.position = RedTeamSpawnPts[randomIndex].transform.position;
        }
    }


}
