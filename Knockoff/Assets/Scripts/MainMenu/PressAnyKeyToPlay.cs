using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            Invoke("LoadScene", 0.5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
