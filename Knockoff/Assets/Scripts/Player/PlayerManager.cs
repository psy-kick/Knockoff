using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

namespace KnockOff.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    //[RequireComponent(typeof(PlayerAnimatorManager))]
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region Private Fields

        private PlayerMovement playerMovement;

        [SerializeField]
        Item[] items;

        int itemIndex;
        int previousItem = -1;

        #endregion

        public bool IsFiring { get; set; }      //networked


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

            /*
            if (!TryGetComponent(out playerAnimatorManager))
                Debug.LogError("<Color=Red><a>Missing</a></Color> Player Animation Manager Component on playerPrefab.", this);*/
        }
        private void Start()
        {
            EquipItem(0);
        }
        private void Update()
        {
            //GameManager.Instance.LeaveRoom() -> losing condition

            if (photonView.IsMine)
            {
                ProcessInputs();
                WeaponSwitchInputs();
            }

        }
        private void WeaponSwitchInputs()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    photonView.RPC("EquipItem",RpcTarget.All,i);
                    break;
                }
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (itemIndex >= items.Length - 1)
                {
                    photonView.RPC("EquipItem", RpcTarget.All, 0);
                }
                else
                {
                    photonView.RPC("EquipItem", RpcTarget.All, itemIndex + 1);
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (itemIndex <= 0)
                {
                    photonView.RPC("EquipItem", RpcTarget.All, items.Length - 1);
                }
                else
                {
                    photonView.RPC("EquipItem", RpcTarget.All, itemIndex - 1);
                }
            }
        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Knock off player if the collider is from opposite team
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)    // we dont' do anything if we are not the local player.
                return;

        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)    // we dont' do anything if we are not the local player.
                return;

        }

        #endregion


        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {    // We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(playerMovement.isGrounded);
            }
            else
            {    // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.playerMovement.isGrounded = (bool)stream.ReceiveNext();
            }
        }

        #endregion

        public void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
        }
        [PunRPC]
        void EquipItem(int  index)
        {
            if(index == previousItem)
            {
                return;
            }
            itemIndex= index;

            items[itemIndex].itemPrefab.SetActive(true);

            if (previousItem != -1)
            {
                items[previousItem].itemPrefab.SetActive(false);
            }
            previousItem = itemIndex;
        }
    }
}
