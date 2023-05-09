using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using KnockOff;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public float timerDuration = 600f; // 10 minutes in seconds

    private float currentTimer;
    private InGameUIHandler gameUIHandler;

    [SerializeField] private GameObject gameOverCanvasPrefab;

    private void Awake()
    {
        gameUIHandler = FindObjectOfType<InGameUIHandler>();
    }

    private void Start()
    {
        currentTimer = timerDuration;

        // Update the timer display
        gameUIHandler.timerTxt.text = FormatTime(currentTimer);
    }

    private void Update()
    {
        if (GameManager.instance.isMatchPlaying)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;

                // Update the timer display across all clients
                photonView.RPC("UpdateTimerDisplay", RpcTarget.All, currentTimer);
            }
            else
            {
                // End the match and display the results
                photonView.RPC("EndMatch", RpcTarget.All);
            }
        }
    }

    // Called by an RPC to update the timer display across all clients
    [PunRPC]
    void UpdateTimerDisplay(float timeRemaining)
    {
        // Update the timer display
        gameUIHandler.timerTxt.text = FormatTime(timeRemaining);
    }

    // Called by an RPC to end the match and display the results
    [PunRPC]
    void EndMatch()
    {
        // End the match and display the results here
        GameManager.instance.isMatchPlaying = false;
        PhotonNetwork.Instantiate(gameOverCanvasPrefab.name, Vector3.zero, Quaternion.identity);
    }

    private string FormatTime(float time)
    {
        // Format the time remaining as minutes and seconds with leading zeros
        int minutes = Mathf.FloorToInt(currentTimer / 60.0f);
        int seconds = Mathf.FloorToInt(currentTimer % 60.0f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        return timeString;
    }
}
