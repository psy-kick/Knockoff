using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using KnockOff.Game;
using KnockOff.Player;

public class InGameUIHandler : MonoBehaviourPunCallbacks
{
    [Header("Score Properties")]
    [SerializeField] private TextMeshProUGUI team1ScoreTxt;
    [SerializeField] private TextMeshProUGUI team2ScoreTxt;

    [Header("Timer Properties")]
    public TextMeshProUGUI timerTxt;

    [Header("Coins Properties")]
    [SerializeField] private TextMeshProUGUI totalCoinsForPlayerTxt;

    public void UpdateScoresUI()
    {
        Debug.Log("UPDATE SCORE");
        // Update team scores displayed in the game UI
        team1ScoreTxt.text = ScoreManager.instance.team1Score.ToString();
        team2ScoreTxt.text = ScoreManager.instance.team2Score.ToString();
    }

    public void UpdateCoinsUI()
    {
        GameObject local = PhotonNetwork.LocalPlayer.TagObject as GameObject;
        totalCoinsForPlayerTxt.text = local.GetComponent<PlayerManager>().totalCoins.ToString();
    }

}
