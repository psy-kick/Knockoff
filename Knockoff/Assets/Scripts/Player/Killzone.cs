using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviourPunCallbacks
{
    [SerializeField] private float respawnCooldown = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null && other.gameObject.tag != "Player")
            return;

        GameObject player = other.gameObject;
        player.SetActive(false);
        StartCoroutine(RespawnPlayer(player));
    }


    private IEnumerator RespawnPlayer(GameObject player)
    {
        // Wait for the respawn cooldown
        yield return new WaitForSeconds(respawnCooldown);

        // Move the player to the spawn position and reactivate it
        player.SetActive(true);
        player.GetComponent<PlayerRespawn>().Respawn();
    }
}
