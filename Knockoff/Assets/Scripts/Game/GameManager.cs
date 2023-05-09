using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnockOff
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static GameManager instance { get; private set; }

        //Persistent Public Properties
        public int selectedCharacterIndex { get; set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

}
