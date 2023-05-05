using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenuHandler : MonoBehaviour
{
    #region Button References
    public void play()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("its connected");
            PhotonNetwork.LoadLevel("Lobby");
        }
        else
        {
            Debug.Log("its Dconnected");
            PhotonNetwork.LoadLevel("Launcher");
        }
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    #endregion
}
