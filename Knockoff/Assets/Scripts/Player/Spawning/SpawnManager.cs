using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] BlueTeamSpawnPts;
    [SerializeField] private SpawnPoint[] RedTeamSpawnPts;

    public static UnityEvent<Transform, PhotonTeam> respawnPlayer;

    private Vector3 localSpawnOffset = new Vector3(0, 0, 1);

    private void Awake()
    {
        respawnPlayer = new UnityEvent<Transform, PhotonTeam>();
    }

    private void OnEnable()
    {
        PhotonTeamsManager.PlayerJoinedTeam += SpawnPlayers;
        respawnPlayer.AddListener(RespawnPlayer);
    }

    private void OnDisable()
    {
        PhotonTeamsManager.PlayerJoinedTeam -= SpawnPlayers;
        respawnPlayer.RemoveListener(RespawnPlayer);
    }

    public void SpawnPlayers(Photon.Realtime.Player p, PhotonTeam team)
    {
        if (p != PhotonNetwork.LocalPlayer) return;      // exit if player is not the local player

        if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[0])
        {
            int randomIndex = Random.Range(0, BlueTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            Transform spawnPoint = BlueTeamSpawnPts[randomIndex].transform;
  
            player.transform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset);
            player.transform.rotation = spawnPoint.localRotation;
        }
        else if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[1])
        {
            int randomIndex = Random.Range(0, RedTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            Transform spawnPoint = RedTeamSpawnPts[randomIndex].transform;

            player.transform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset);
            player.transform.rotation = spawnPoint.localRotation;
        }
    }

    public void RespawnPlayer(Transform playerTransform, PhotonTeam playerTeam)
    {
        if (!playerTransform.GetComponent<PhotonView>().IsMine) return;      // exit if player is not the local player

        if (playerTeam == PhotonTeamsManager.Instance.GetAvailableTeams()[0])
        {
            int randomIndex = Random.Range(0, BlueTeamSpawnPts.Length);
            Transform spawnPoint = BlueTeamSpawnPts[randomIndex].transform;

            playerTransform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset);
            playerTransform.rotation = spawnPoint.localRotation;
        }
        else if (playerTeam == PhotonTeamsManager.Instance.GetAvailableTeams()[1])
        {
            int randomIndex = Random.Range(0, RedTeamSpawnPts.Length);
            Transform spawnPoint = RedTeamSpawnPts[randomIndex].transform;

            playerTransform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset);
            playerTransform.rotation = spawnPoint.localRotation;
        }
    }

}
