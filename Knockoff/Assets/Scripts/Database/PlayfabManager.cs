using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text Messagetext;
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField EmailInputRegister;
    public TMP_InputField PasswordInputRegister;
    public TMP_Text MessagetextRegister;
    public TMP_InputField EmailInputReset;
    public TMP_Text MessagetextReset;

    public GameObject RegisterPanel;
    public GameObject LoginPanel;
    public GameObject ResetPanel;

    public void SwitchTORegisterPage()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }
    public void SwitchToResetPass()
    {
        LoginPanel.SetActive(false);
        ResetPanel.SetActive(true);
    }
    public void RegisterButton()
    {
        if (PasswordInputRegister.text.Length < 6)
        {
            MessagetextRegister.text = "Password too short need to be more than 6 digits";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = EmailInputRegister.text,
            Password = PasswordInputRegister.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailInput.text,
            Password = PasswordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = EmailInputReset.text,
            TitleId = "11017"
    };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        MessagetextReset.text = "Reset link sent to mail!";
        StartCoroutine(PanelShifter());
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Messagetext.text = "Logged In!";
        StartCoroutine(SceneLoader());
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        MessagetextRegister.text = "Registered and Logged In";
        StartCoroutine(SceneLoader());
    }
    private void OnError(PlayFabError error)
    {
        Messagetext.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator PanelShifter()
    {
        yield return new WaitForSeconds(1f);
        ResetPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucess, OnError);
    }
    private void OnSucess(LoginResult result)
    {
        Debug.Log("Success");
    }
}
