using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using KnockOff.Game;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject CharacterSelectToDisplay = default;
    [SerializeField]
    private Transform CharacterPreviewParent = default;
    [SerializeField]
    private TMP_Text CharacterNameText = default;
    [SerializeField]
    private PlayerCharacters[] characterList = default;

    private int CurrentCharaterIndex = 0;
    private List<GameObject> CharacterInstances = new List<GameObject>();

    [HideInInspector]
    public GameObject CharacterSelected;

    private int playersDoneSelecting = 0;

    // Start is called before the first frame update
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
        {
            playersDoneSelecting = (int)value;
        }

    }

    public void OnClickSelected(int characterIndex)
    {
        CharacterSelected = characterList[characterIndex].GameCharaterPrefab;

        // Get the current value of the custom room property
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("NumSelectedCharacters", out object value))
        {
            playersDoneSelecting = (int)value;
        }

        // Increment the number of selected characters
        playersDoneSelecting++;

        // Set the custom room property to the updated value
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();
        props.Add("NumSelectedCharacters", playersDoneSelecting);
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        // Check if all players have selected their character
        if (playersDoneSelecting == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // Load the waiting room scene
                PhotonNetwork.LoadLevel("WaitingRoom");
            }
            else
            {
                // Send an RPC to the master client to load the next level
                GetComponent<PhotonView>().RPC("LoadLevelRPC", RpcTarget.MasterClient, "WaitingRoom");
            }
        }
    }

    [PunRPC]
    void LoadLevelRPC(string levelName)
    {
        // The master client loads the specified level
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(levelName);
        }
    }

    public void OnClickRight()
    {
        CharacterInstances[CurrentCharaterIndex].SetActive(false);
        CurrentCharaterIndex = (CurrentCharaterIndex + 1) % CharacterInstances.Count;
        CharacterInstances[CurrentCharaterIndex].SetActive(true);
        CharacterNameText.text = characterList[CurrentCharaterIndex].CharacterName;
        PlayerPrefs.SetInt("CharacterIndex", CurrentCharaterIndex);
    }
    public void OnClickLeft()
    {
        CharacterInstances[CurrentCharaterIndex].SetActive(false);
        CurrentCharaterIndex--;
        if (CurrentCharaterIndex < 0)
        {
            CurrentCharaterIndex += CharacterInstances.Count;
        }
        CharacterInstances[CurrentCharaterIndex].SetActive(true);
        CharacterNameText.text = characterList[CurrentCharaterIndex].CharacterName;
        PlayerPrefs.SetInt("CharacterIndex", CurrentCharaterIndex);
    }
}
