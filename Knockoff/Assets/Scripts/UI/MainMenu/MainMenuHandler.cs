using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    private string sceneName;

    #region Button References
    public void GoPlay()
    {
        if (PhotonNetwork.IsConnected)
        {
            m_AudioSource.Play();
            sceneName = "Lobby";
            Invoke("WaitForPhotonScene", 0.5f);
        }
        else
        {
            m_AudioSource.Play();
            sceneName = "Launcher";
            Invoke("WaitForPhotonScene", 0.5f);
        }
    }

    public void GoSettings()
    {
        m_AudioSource.Play();
        sceneName = "Settings";
        Invoke("WaitForLoadScene", 0.5f);
    }

    public void GoGuide()
    {
        m_AudioSource.Play();
        sceneName = "Guide";
        Invoke("WaitForLoadScene", 0.5f);
    }

    public void GoCredits()
    {
        m_AudioSource.Play();
        sceneName = "Credits";
        Invoke("WaitForLoadScene", 0.5f);
    }
    public void GoToScoreboard()
    {
        m_AudioSource.Play();
        sceneName = "ScoreBoard";
        Invoke("WaitForLoadScene", 0.5f);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void WaitForLoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void WaitForPhotonScene()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    #endregion
}
