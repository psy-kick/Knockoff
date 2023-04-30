using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Charater Selection/Charater")]
public class PlayerCharacters : ScriptableObject
{
    [SerializeField] private string characterName = default;
    [SerializeField] private GameObject charaterPreviewPrefab = default;
    [SerializeField] private GameObject gameCharaterPrefab = default;

    public string CharacterName => characterName;
    public GameObject CharaterPreviewPrefab => charaterPreviewPrefab;
    public GameObject GameCharaterPrefab => gameCharaterPrefab;
}
