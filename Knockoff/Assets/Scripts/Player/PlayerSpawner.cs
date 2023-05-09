using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace KnockOff.Player
{
    public class PlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject[] playerPrefab;

        private int playersInstantiatedCount = 0;

        [Tooltip("The name of the arena scene")]
        public string arenaSceneTxt = "GameScene";

        public void StartGame()
        {
            CreatePlayerPrefab();
        }

        public GameObject CreatePlayerPrefab()
        {
            GameObject player = null;

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    player = PhotonNetwork.Instantiate(playerPrefab[GameManager.instance.selectedCharacterIndex].name, Vector3.zero, Quaternion.identity, 0);
                    photonView.RPC("IncrementPlayersInstantiatedCount", RpcTarget.All);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

            return player;
        }

        [PunRPC]
        private void IncrementPlayersInstantiatedCount()
        {
            playersInstantiatedCount++;

            if (playersInstantiatedCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                AudioManager.instance.ChangeToGameplayMusic();
                LoadArena();
            }
        }

        public void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == LobbyManager.MaxPlayersPerRoom)
            {
                Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
                PhotonNetwork.LoadLevel(arenaSceneTxt);   // we don't use Unity directly, because we want to rely on Photon to load this level on all connected clients in the room,
                                                         // since we've enabled PhotonNetwork.AutomaticallySyncScene for this Game. (PhotonNetwork.CurrentRoom.PlayerCount)
            }
        }
    }

}