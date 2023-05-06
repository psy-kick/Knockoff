using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace KnockOff.Launcher
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [Header("UI Fields")]
        [Tooltip("The UI Panel to let the user enter name, connect and play")]
        [SerializeField] private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField] private GameObject progressLabel;

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        /// <summary>
        /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
        /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
        /// Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;

        [SerializeField] private AudioSource buttonSound;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region MonobehaviourPun Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            // we don't want to do anything if we are not attempting to join a room.
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            SceneManager.LoadScene("Lobby");
            //uncomment this later if needed
            //if (isConnecting)
            //{
            //    // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            //    PhotonNetwork.JoinRandomRoom();
            //    isConnecting = false;
            //}
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);

            isConnecting = false;
        }
        #endregion


        #region Public Methods

        public void Connect()
        {
            buttonSound.Play();
            Invoke("OnConnectClicked", 0.5f);
        }

        public void OnConnectClicked()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        #endregion
    }
}
