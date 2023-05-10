using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
using KnockOff.Game;

public class GameOverUIHandler : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] private TextMeshProUGUI team1Name;
    [SerializeField] private TextMeshProUGUI team2Name;
    [SerializeField] private TextMeshProUGUI score1;
    [SerializeField] private TextMeshProUGUI score2;

    PlayfabManager playfabManager;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        team1Name.text = PhotonTeamsManager.Instance.GetAvailableTeams()[0].Name;
        team2Name.text = PhotonTeamsManager.Instance.GetAvailableTeams()[1].Name;
        score1.text = ScoreManager.instance.team1Score.ToString();
        score2.text = ScoreManager.instance.team2Score.ToString();

        //need to show and save who's the winner
        playfabManager = FindObjectOfType<PlayfabManager>();
        playfabManager.SendScoreSystemTeam1(ScoreManager.instance.team1Score);
        playfabManager.SendScoreSystemTeam2(ScoreManager.instance.team2Score);
    }

    public void GoMainMenu()
    {
        AudioManager.instance.ChangeToMainMenuMusic();
        PhotonNetwork.Disconnect();
    }

    public void PlayAgain()
    {
        //need to restart everything
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
