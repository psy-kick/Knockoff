using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using KnockOff;
using KnockOff.Player;
using Photon.Pun.UtilityScripts;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject CharacterSelectToDisplay = default;
    [SerializeField] private Transform CharacterPreviewParent = default;
    [SerializeField] private TMP_Text CharacterNameText = default;
    [SerializeField] private PlayerCharacters[] characterList = default;

    private List<GameObject> CharacterInstances = new List<GameObject>();
    private PlayerSpawner playerSpawner;

    private int CurrentCharaterIndex = 0;
    private int playersDoneSelecting = 0;

    [HideInInspector]
    public GameObject CharacterSelected;

    private void Awake()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }

    void Start()
    {
        if (CharacterPreviewParent.childCount == 0)
        {
            foreach (var character in characterList)
            {
                GameObject CharacterInstance = Instantiate(character.CharaterPreviewPrefab, CharacterPreviewParent);
                CharacterInstance.SetActive(false);
                CharacterInstances.Add(CharacterInstance);
            }
            CharacterInstances[CurrentCharaterIndex].SetActive(true);
            CharacterNameText.text = characterList[CurrentCharaterIndex].CharacterName;

            CharacterSelectToDisplay.SetActive(true);
        }

        // Get the current value of the custom room property
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("NumSelectedCharacters", out object value))
            playersDoneSelecting = (int)value;

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            p.LeaveCurrentTeam();
    }

    public void OnClickSelected(int characterIndex)
    {
        CharacterSelected = characterList[characterIndex].GameCharaterPrefab;

        // Get the current value of the custom room property
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("NumSelectedCharacters", out object value))
            playersDoneSelecting = (int)value;

        // Increment the number of selected characters
        playersDoneSelecting++;

        // Set the custom room property to the updated value
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();
        props.Add("NumSelectedCharacters", playersDoneSelecting);
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        GameManager.instance.selectedCharacterIndex = CurrentCharaterIndex;

        // Check if all players have selected their character
        if (playersDoneSelecting == PhotonNetwork.CurrentRoom.MaxPlayers)
            photonView.RPC("StartGameOnAllClients", RpcTarget.All);
    }

    [PunRPC]
    public void StartGameOnAllClients()
    {
        playerSpawner.StartGame();
    }

    public void OnClickRight()
    {
        CharacterInstances[CurrentCharaterIndex].SetActive(false);
        CurrentCharaterIndex = (CurrentCharaterIndex + 1) % CharacterInstances.Count;
        CharacterInstances[CurrentCharaterIndex].SetActive(true);
        CharacterNameText.text = characterList[CurrentCharaterIndex].CharacterName;
    }

    public void OnClickLeft()
    {
        CharacterInstances[CurrentCharaterIndex].SetActive(false);
        CurrentCharaterIndex--;
        if (CurrentCharaterIndex < 0)
            CurrentCharaterIndex += CharacterInstances.Count;

        CharacterInstances[CurrentCharaterIndex].SetActive(true);
        CharacterNameText.text = characterList[CurrentCharaterIndex].CharacterName;
    }
}
