using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToPlay : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            buttonSound.Play(); 
            Invoke("LoadScene", 0.5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("LoginSystem");
    }
}
