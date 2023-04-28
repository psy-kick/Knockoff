using KnockOff.Player;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] BlueTeamSpawnPts;
    [SerializeField] private SpawnPoint[] RedTeamSpawnPts;

    public static UnityEvent<Transform, PhotonTeam> spawnPlayer;

    private Vector3 localSpawnOffset = new Vector3(0, 0, 1);
    private float spawnForwardOffset = 2.0f;

    private void Awake()
    {
        spawnPlayer = new UnityEvent<Transform, PhotonTeam>();
    }

    private void OnEnable()
    {
        PhotonTeamsManager.PlayerJoinedTeam += SpawnPlayers;
        spawnPlayer.AddListener(RespawnPlayer);
    }

    private void OnDisable()
    {
        PhotonTeamsManager.PlayerJoinedTeam -= SpawnPlayers;
        spawnPlayer.RemoveListener(RespawnPlayer);
    }

    public void SpawnPlayers(Photon.Realtime.Player p, PhotonTeam team)
    {
        if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[0])
        {
            int randomIndex = Random.Range(0, BlueTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            Transform spawnPoint = BlueTeamSpawnPts[randomIndex].transform;

            player.transform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset) + spawnPoint.forward * spawnForwardOffset;
            player.transform.rotation = spawnPoint.localRotation;

            player.transform.GetComponent<PlayerMovement>().SetRotation(spawnPoint.localRotation);

            StartCoroutine(EnableRotationAfterDelay(1f, player.transform));
        }
        
        if (team == PhotonTeamsManager.Instance.GetAvailableTeams()[1])
        {
            int randomIndex = Random.Range(0, RedTeamSpawnPts.Length);
            GameObject player = p.TagObject as GameObject;
            Transform spawnPoint = RedTeamSpawnPts[randomIndex].transform;

            player.transform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset) + spawnPoint.forward * spawnForwardOffset;
            player.transform.rotation = spawnPoint.localRotation;

            player.transform.GetComponent<PlayerMovement>().SetRotation(spawnPoint.localRotation);

            StartCoroutine(EnableRotationAfterDelay(1f, player.transform));
        }
    }

    public void RespawnPlayer(Transform playerTransform, PhotonTeam playerTeam)
    {
        if (playerTeam == PhotonTeamsManager.Instance.GetAvailableTeams()[0])
        {
            int randomIndex = Random.Range(0, BlueTeamSpawnPts.Length);
            Transform spawnPoint = BlueTeamSpawnPts[randomIndex].transform;
            
            playerTransform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset) + spawnPoint.forward * spawnForwardOffset;
            playerTransform.rotation = spawnPoint.localRotation;

            playerTransform.GetComponent<PlayerMovement>().SetRotation(spawnPoint.localRotation);

            StartCoroutine(EnableRotationAfterDelay(1f, playerTransform));
        }

        if (playerTeam == PhotonTeamsManager.Instance.GetAvailableTeams()[1])
        {
            int randomIndex = Random.Range(0, RedTeamSpawnPts.Length);
            Transform spawnPoint = RedTeamSpawnPts[randomIndex].transform;

            playerTransform.position = spawnPoint.position + spawnPoint.TransformVector(localSpawnOffset) + spawnPoint.forward * spawnForwardOffset;
            playerTransform.rotation = spawnPoint.localRotation;

            playerTransform.GetComponent<PlayerMovement>().SetRotation(spawnPoint.localRotation);

            StartCoroutine(EnableRotationAfterDelay(1f, playerTransform));
        }
    }

    IEnumerator EnableRotationAfterDelay(float delay, Transform p)      //is gonna be accompanied by UI with a countdown for match start
    {
        yield return new WaitForSecondsRealtime(delay);

        p.GetComponent<PlayerMovement>().useMouseInput = true;
    }


}
