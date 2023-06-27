using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using KnockOff.Player;

namespace KnockOff.Game
{
    public class Coin : MonoBehaviourPunCallbacks
    {
        [SerializeField] private int coinType;
        [SerializeField] private int coinValue;

        private AudioSource coinAudioSource;

        private InGameUIHandler InGameUIHandler;

        private void Start()
        {
            coinAudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.tag == "Player" && gameObject != null && transform.parent != null)
            {
                coinAudioSource.Play();
                other.transform.parent.GetComponent<PlayerManager>().totalCoins += coinValue;
                InGameUIHandler = FindObjectOfType<InGameUIHandler>();
                InGameUIHandler.UpdateCoinsUI();
                //photonView.RPC("RespawnCoin", RpcTarget.MasterClient, coinType, transform.parent.position);
                RespawnCoin(coinType, transform.parent.position);
                Invoke("WaitForDestroy", 0.2f);
            }

            if (transform.parent == null)
                PhotonNetwork.Destroy(gameObject);
        }

        private void WaitForDestroy()
        {
            transform.SetParent(null);
            PhotonNetwork.Destroy(gameObject);
        }

        public void RespawnCoin(int _coinType, Vector3 _loc)
        {
            CoinsManager.instance.RespawnCoin(_coinType, _loc);
        }
    }
}

