﻿using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KnockOff.Player
{
    public class PlayerNameText : MonoBehaviourPunCallbacks
    {
        private TextMeshPro userNameTxt;

        private void Start()
        {
            userNameTxt = GetComponent<TextMeshPro>();
        }

        private void Update()
        {
            if (GameManager.instance.isMatchPlaying)
            {
                photonView.RPC("SetPlayerUsernameForOtherPlayers", RpcTarget.All);
            }
        }

        [PunRPC]
        public void SetPlayerUsernameForOtherPlayers()
        {
            SetText(photonView.Owner.NickName);
            SetColour(photonView.Owner.GetPhotonTeam().Name);

            if (photonView.IsMine)
            {
                userNameTxt.enabled = false;
            }
            else
            {
                userNameTxt.enabled = true;
            }
        }

        public void SetText(string s)
        {
            userNameTxt.text = s;
        }

        public void SetColour(string s)
        {
            Color color;

            if (ColorUtility.TryParseHtmlString(GetColorString(s), out color))
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