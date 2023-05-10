using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Game
{
    public class Coin : MonoBehaviourPunCallbacks
    {
        [SerializeField] private int coinType;
        [SerializeField] private float coinValue;

        private AudioSource coinAudioSource;

        private void Start()
        {
            coinAudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.tag == "Player")
            {
                coinAudioSource.Play();
                photonView.RPC("RespawnCoin", RpcTarget.All, coinType, transform.parent.position);
                Invoke("WaitForDestroy", 0.2f);
            }
        }

        private void WaitForDestroy()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        public void RespawnCoin(int _coinType, Vector3 _loc)
        {
            CoinsManager.instance.RespawnCoin(_coinType, _loc);
        }
    }
}

