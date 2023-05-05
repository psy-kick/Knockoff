using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BackButton : MonoBehaviour
{
    public void OnclickBack()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
