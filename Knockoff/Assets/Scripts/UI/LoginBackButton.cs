using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginBackButton : MonoBehaviour
{
    [SerializeField] GameObject PanelToSwitch;
    [SerializeField] GameObject CurrentPanel;
    // Start is called before the first frame update
    public void OnclickBack()
    {
        Invoke("WaitForSceneLoad", 0.5f);
    }

    private void WaitForSceneLoad()
    {
        CurrentPanel.SetActive(false);
        PanelToSwitch.SetActive(true);
    }
}
