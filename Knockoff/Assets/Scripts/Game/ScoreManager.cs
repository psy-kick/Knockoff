using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager instance { get; private set; }

    private const string TEAM_1_SCORE_PROPERTY_NAME = "Team1Score";
    private const string TEAM_2_SCORE_PROPERTY_NAME = "Team2Score";

    public int team1Score { get; private set; }
    public int team2Score { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    { 
        // Set initial scores to 0
        team1Score = 0;
        team2Score = 0;

        // Set initial room properties for team scores
        ExitGames.Client.Photon.Hashtable initialProps = new ExitGames.Client.Photon.Hashtable() { { TEAM_1_SCORE_PROPERTY_NAME, team1Score }, { TEAM_2_SCORE_PROPERTY_NAME, team2Score } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(initialProps);
    }


    public void UpdateTeamScore(int teamNumber, int scoreChange)
    {
        if (teamNumber == PhotonTeamsManager.Instance.GetAvailableTeams()[0].Code)
        {
            team1Score += scoreChange;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable() { { TEAM_1_SCORE_PROPERTY_NAME, team1Score } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
        else if (teamNumber == PhotonTeamsManager.Instance.GetAvailableTeams()[1].Code)
        {
            team2Score += scoreChange;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable() { { TEAM_2_SCORE_PROPERTY_NAME, team2Score } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }

        //if we wanted to add more teams
        /*
        foreach (PhotonTeam team in PhotonTeamsManager.Instance.GetAvailableTeams())
        {
            if (team.Code == teamNumber)
            {

            }
        }*/
    }


    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(TEAM_1_SCORE_PROPERTY_NAME))
        {
            team1Score = (int)propertiesThatChanged[TEAM_1_SCORE_PROPERTY_NAME];
        }

        if (propertiesThatChanged.ContainsKey(TEAM_2_SCORE_PROPERTY_NAME))
        {
            team2Score = (int)propertiesThatChanged[TEAM_2_SCORE_PROPERTY_NAME];
        }
    }

}
