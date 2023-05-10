using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Game.UI
{
    public class WinningLosingUI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject[] teamCrowns;
        [SerializeField] private GameObject matchDrawTxt;

        private void Start()
        {
            if (ScoreManager.instance.ReturnTeamWinner() > 0)
                teamCrowns[ScoreManager.instance.ReturnTeamWinner() - 1].SetActive(true);
            else
                matchDrawTxt.SetActive(true);
        }
    }
}
