using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Photon.Pun;
using KnockOff.Game;

public class CharacterSelect : MonoBehaviour
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
    }
    public void OnClickSelected(int characterIndex)
    {
        CharacterSelected = characterList[characterIndex].GameCharaterPrefab;
        PhotonNetwork.LoadLevel("WaitingRoom");
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
