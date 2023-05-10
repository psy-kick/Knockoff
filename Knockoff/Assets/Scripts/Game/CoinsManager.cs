using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Game
{
    public class CoinsManager : MonoBehaviourPunCallbacks
    {
        public static CoinsManager instance { get; private set; }

        [SerializeField] private GameObject[] coinsPrefab;

        [SerializeField] private Transform goldCoinsSpawnPtsHolder;
        [SerializeField] private Transform silverCoinsSpawnPtsHolder;
        [SerializeField] private Transform bronzeCoinsSpawnPtsHolder;

        [SerializeField] private float respawnCooldown = 15f;

        private InGameUIHandler InGameUIHandler;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            InGameUIHandler = FindObjectOfType<InGameUIHandler>();
        }

        public void RespawnCoin(int coinType, Vector3 spawnPoint)
        {
            StartCoroutine(WaitForCooldown(coinType, spawnPoint));
        }

        IEnumerator WaitForCooldown(int c, Vector3 loc)
        {
            yield return new WaitForSeconds(respawnCooldown);
            GameObject coin = PhotonNetwork.Instantiate(coinsPrefab[c - 1].name, loc, Quaternion.identity);

            if (c == 1)
            {
                for (int i = 0; i < bronzeCoinsSpawnPtsHolder.childCount; i++)
                {
                    if (bronzeCoinsSpawnPtsHolder.GetChild(i).childCount == 0)
                    {
                        coin.transform.SetParent(bronzeCoinsSpawnPtsHolder.GetChild(i));
                        break;
                    }
                }
            }
            else if (c == 2)
            {
                for (int i = 0; i < silverCoinsSpawnPtsHolder.childCount; i++)
                {
                    if (silverCoinsSpawnPtsHolder.GetChild(i).childCount == 0)
                    {
                        coin.transform.SetParent(silverCoinsSpawnPtsHolder.GetChild(i));
                        break;
                    }
                }
            }
            else if (c == 3)
            {
                for (int i = 0; i < goldCoinsSpawnPtsHolder.childCount; i++)
                {
                    if (goldCoinsSpawnPtsHolder.GetChild(i).childCount == 0)
                    {
                        coin.transform.SetParent(goldCoinsSpawnPtsHolder.GetChild(i));
                        break;
                    }
                }
            }
        }
    }
}

