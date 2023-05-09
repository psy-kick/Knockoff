using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BackButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OnclickBack()
    {
        Invoke("WaitForSceneLoad", 0.5f);
    }

    private void WaitForSceneLoad()
    {
        SceneManager.LoadScene(sceneName);
    }
}
