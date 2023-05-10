using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace KnockOff.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #region Public Fields
        public PlayerManager localPlayer { get; private set; }

        public int totalCoins { get; set; }
        public PhotonTeam playerTeam { get; private set; }
        public string playerUsername { get; private set; }

        #endregion

        #region Private Fields

        private PlayerMovement playerMovement;
        private PlayerAttack playerAttack;

        #endregion

        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
                localPlayer = this;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            if (!TryGetComponent(out playerMovement))
                Debug.LogError("<Color=Red><a>Missing</a></Color> Player Movement Component on playerPrefab.", this);
            
            if (!TryGetComponent(out playerAttack))
                Debug.LogError("<Color=Red><a>Missing</a></Color> Player Attack Component on playerPrefab.", this);

            PhotonTeamsManager.PlayerJoinedTeam += AlertPlayersAboutTeams;
        }

        private void OnDestroy()
        {
            PhotonTeamsManager.PlayerJoinedTeam -= AlertPlayersAboutTeams;
        }

        private void AlertPlayersAboutTeams(Photon.Realtime.Player p, PhotonTeam team)
        {
            if (photonView.IsMine)
            {
                playerTeam = team;
                playerUsername = p.NickName;
                Debug.LogFormat("{0}, You have been assigned to the <Color={1}><a>{1}</a></Color> team.", p.NickName, playerTeam.Name);

                // Set the TagObject property to the player's GameObject
                p.TagObject = this.gameObject;
            }
        }

        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {    // We own this player: send the others our data
                stream.SendNext(playerAttack.IsFiring);
                stream.SendNext(playerMovement.isGrounded);
                stream.SendNext(playerMovement.isSprinting);
            }
            else
            {    // Network player, receive data
                this.playerAttack.IsFiring = (bool)stream.ReceiveNext();
                this.playerMovement.isGrounded = (bool)stream.ReceiveNext();
                this.playerMovement.isSprinting = (bool)stream.ReceiveNext();
            }
        }

        #endregion


    }
}
