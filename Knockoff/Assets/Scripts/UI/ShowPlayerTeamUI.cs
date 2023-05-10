using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

namespace KnockOff.Player.UI
{
    public class ShowPlayerTeamUI : MonoBehaviourPunCallbacks
    {
        private TextMeshProUGUI teamName;

        private void Start()
        {
            teamName = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            photonView.RPC("SetTeamNameAtStart", RpcTarget.All);
        }

        [PunRPC]
        public void SetTeamNameAtStart()
        {
            if (PhotonNetwork.LocalPlayer.GetPhotonTeam() != null)
            {
                teamName.text = "You are on the " + PhotonNetwork.LocalPlayer.GetPhotonTeam().Name + " Team";
                SetColour(PhotonNetwork.LocalPlayer.GetPhotonTeam().Name);
            }
        }

        public void DisableObject()
        {
            gameObject.SetActive(false);
        }

        public void SetColour(string s)
        {
            Color color;

            if (ColorUtility.TryParseHtmlString(GetColorString(s), out color))
                teamName.color = color;
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

