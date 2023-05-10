using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnockOff.Game
{
    public class ScoreManager : MonoBehaviourPunCallbacks
    {
        public static ScoreManager instance { get; private set; }

        public int team1Score { get; private set; }
        public int team2Score { get; private set; }

        private InGameUIHandler InGameUIHandler;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            InGameUIHandler = FindObjectOfType<InGameUIHandler>();
        }

        private void Start()
        {
            // Set initial scores to 0
            team1Score = 0;
            team2Score = 0;

            // Update the scores across all clients
            photonView.RPC("UpdateScores", RpcTarget.All, team1Score, team2Score);
        }

        public void UpdateTeamScore(int teamNumber, int scoreChange)
        {
            if (teamNumber == PhotonTeamsManager.Instance.GetAvailableTeams()[0].Code)
            {
                team1Score += scoreChange;
            }
            else if (teamNumber == PhotonTeamsManager.Instance.GetAvailableTeams()[1].Code)
            {
                team2Score += scoreChange;
            }

            // Update the scores across all clients
            photonView.RPC("UpdateScores", RpcTarget.All, team1Score, team2Score);
        }

        [PunRPC]
        private void UpdateScores(int team1Score, int team2Score)
        {
            this.team1Score = team1Score;
            this.team2Score = team2Score;

            //call ui
            InGameUIHandler.UpdateScoresUI();
        }

        public int ReturnTeamWinner()
        {
            if (team1Score > team2Score)
                return 1;
            else if (team2Score > team1Score)
                return 2;
            else
                return 0;
        }


    }
}
