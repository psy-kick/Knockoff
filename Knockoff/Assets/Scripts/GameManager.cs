using KnockOff.Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnockOff.Game
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Public Fields
        public static GameManager Instance { get; private set; }

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        public GameObject crosshair;

        #endregion

        #region Monobehaviour Callbacks

        private void Start()
        {
            Instance = this;

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
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            
            }
        }

        #endregion

    }

}
