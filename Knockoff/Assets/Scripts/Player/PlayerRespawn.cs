using KnockOff.Player;
using KnockOff.Game;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviourPunCallbacks
{
    private PlayerManager playerManager;
    public Photon.Realtime.Player Opponent { get; set; }

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    /// <summary>
    /// Knock off player if the collider is from opposite team
    /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
    /// One could move the collider further away to prevent this or check if the beam belongs to the player.
    /// </summary>
    public void Respawn()
    {
        if (!photonView.IsMine)    // we dont' do anything if we are not the local player.
            return;

        SpawnManager.respawnPlayer?.Invoke(transform, playerManager.playerTeam);

        if (Opponent != null)
        {
            //update scoring system (give point to opposing team)
            ScoreManager.instance.UpdateTeamScore(Opponent.GetPhotonTeam().Code, 1);
            Opponent = null;
        }
    }
}
