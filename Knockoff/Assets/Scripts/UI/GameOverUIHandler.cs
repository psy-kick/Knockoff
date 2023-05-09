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

    private string sceneName;

    private void Start()
    {
        team1Name.text = PhotonTeamsManager.Instance.GetAvailableTeams()[0].Name;
        team2Name.text = PhotonTeamsManager.Instance.GetAvailableTeams()[1].Name;
        score1.text = ScoreManager.instance.team1Score.ToString();
        score2.text = ScoreManager.instance.team2Score.ToString();


        //need to show and save who's the winner
    }

    public void GoMainMenu()
    {
        sceneName = "MainMenu";
        Invoke("WaitForPhotonScene", 0.5f);
    }

    public void PlayAgain()
    {
        //need to restart everything
    }

    private void WaitForPhotonScene()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
