using KnockOff.Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviourPunCallbacks
{
    [SerializeField] private float respawnCooldown = 3f;
    private InGameUIHandler InGameUIHandler;

    private void Start()
    {
        InGameUIHandler = FindObjectOfType<InGameUIHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null || other.transform.parent.tag != "Player")
            return;

        GameObject player = other.transform.parent.gameObject;
        player.transform.rotation = Quaternion.identity;            //reset rotation
        player.transform.GetChild(0).rotation = Quaternion.identity;    //reset rotation
        player.SetActive(false);
        StartCoroutine(RespawnPlayer(player));
    }


    private IEnumerator RespawnPlayer(GameObject player)
    {
        // Wait for the respawn cooldown
        yield return new WaitForSeconds(respawnCooldown);

        // Move the player to the spawn position and reactivate it
        player.SetActive(true);
        player.GetComponent<PlayerManager>().totalCoins = 0;        //reset coins when knocked off
        InGameUIHandler.UpdateCoinsUI();
        player.GetComponent<PlayerRespawn>().Respawn();
    }
}
