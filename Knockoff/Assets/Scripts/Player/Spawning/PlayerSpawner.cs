using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Player
{
    public class PlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject[] playerPrefab;

        private RoomManager roomManager;
        private int playersInstantiatedCount = 0;

        private void Awake()
        {
            roomManager = FindObjectOfType<RoomManager>();
        }

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
                    //int characterIndex = PlayerPrefs.GetInt("CharacterIndex");
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
                roomManager.LoadArena();
            }
        }
    }

}