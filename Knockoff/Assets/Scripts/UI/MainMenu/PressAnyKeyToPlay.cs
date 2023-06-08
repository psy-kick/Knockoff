using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void LoadScene()
    {
        SceneLoader.LoadNextScene();
    }
}
