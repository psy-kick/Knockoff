using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Player
{
    public class PlayerAttack : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Item[] items;
        public bool IsFiring { get; set; }      //networked

        private int itemIndex = 0;
        private float lastSwitchTime = 0f;
        private float switchDelay = 0.25f; // set the delay time to half a second

        private void Start()
        {
            EquipItem(0);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();
                WeaponSwitchInputs();
            }
        }

        public void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    items[itemIndex].Use();
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

        private void WeaponSwitchInputs()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            if (scrollWheel != 0 && Time.time - lastSwitchTime > switchDelay)
            {
                lastSwitchTime = Time.time;
                itemIndex++;

                if (itemIndex == items.Length)
                    itemIndex = 0;

                if (scrollWheel > 0)
                {
                    // Scroll wheel was scrolled up
                    photonView.RPC("EquipItem", RpcTarget.All, itemIndex);
                }
                else if (scrollWheel < 0)
                {
                    // Scroll wheel was scrolled down
                    photonView.RPC("EquipItem", RpcTarget.All, itemIndex);
                }
            }
            else
            {   
                //for switching weapons with number inputs instead of scroll wheel
                for (int i = 0; i < items.Length; i++)
                {
                    if (Input.GetKeyDown((i + 1).ToString()))
                    {
                        itemIndex = i;
                        photonView.RPC("EquipItem", RpcTarget.All, i);
                        break;
                    }
                }
            }
        }

        [PunRPC]
        void EquipItem(int index)
        {
            for (int i = 0; i < items.Length; i++)
                items[i].gameObject.SetActive(false);

            items[index].gameObject.SetActive(true);
        }
    }
}
