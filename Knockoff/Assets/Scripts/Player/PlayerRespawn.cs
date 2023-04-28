using KnockOff.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void Respawn()
    {
        SpawnManager.respawnPlayer?.Invoke(transform, playerManager.playerTeam);

        //update scoring system (give point to opposing team)
    }
}
