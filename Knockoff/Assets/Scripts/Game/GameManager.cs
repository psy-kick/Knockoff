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
        public static GameManager instance { get; private set; }
        public List<GameObject> playerPrefab;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject crosshair;

        #endregion

        #region Monobehaviour Callbacks


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(instance);
            }
        }

        private void Start()
        {
            CreatePlayerPrefab(new Vector3(0f, 5f, 0f));
            AudioManager.instance.ChangeToGameplayMusic();
        }

        public GameObject CreatePlayerPrefab(Vector3 pos)
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
                    int characterIndex = PlayerPrefs.GetInt("CharacterIndex");
                    player = PhotonNetwork.Instantiate(playerPrefab[characterIndex].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }

            }

            return player;
        }

        #endregion

    }

}
