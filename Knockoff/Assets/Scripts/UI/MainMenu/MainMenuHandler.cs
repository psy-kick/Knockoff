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
    public void play()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("its connected");
            m_AudioSource.Play();
            sceneName = "Lobby";
            Invoke("WaitForPhotonScene", 0.5f);
        }
        else
        {
            Debug.Log("its Disconnected");
            m_AudioSource.Play();
            sceneName = "Launcher";
            Invoke("WaitForPhotonScene", 0.5f);
        }
    }
    public void Options()
    {
        m_AudioSource.Play();
        sceneName = "Options";
        Invoke("WaitForLoadScene", 0.5f);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        m_AudioSource.Play();
        sceneName = "Credits";
        Invoke("WaitForLoadScene", 0.5f);
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
