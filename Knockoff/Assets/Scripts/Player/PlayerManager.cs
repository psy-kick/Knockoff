using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;

namespace KnockOff.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region Private Fields

        [SerializeField] private TextMeshPro userNameTxt; 

        private PlayerMovement playerMovement;
        private PlayerAttack playerAttack;

        #endregion

        public PhotonTeam playerTeam { get; private set; }
        public string playerUsername { get; private set; }

        #region Monobehaviour Callbacks

        private void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
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

                //only show username to other players, not myself
                photonView.RPC("SetPlayerNameForOtherPlayers", RpcTarget.Others, playerUsername, playerTeam.Name);
            }
        }

        #endregion


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

        [PunRPC]
        void SetPlayerNameForOtherPlayers(string playerName, string playerTeamName)
        {
            userNameTxt.text = playerName;

            string colorString = playerTeamName;
            Color color;

            if (ColorUtility.TryParseHtmlString(GetColorString(colorString), out color))
                userNameTxt.color = color;
        }

        // Helper function to get the color string for known color names
        private string GetColorString(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "red":
                    return "#FF0000";
                case "green":
                    return "#00FF00";
                case "blue":
                    return "#0000FF";
                // Add more cases for other colors as needed
                default:
                    return "#000000";
            }
        }
    }
}
