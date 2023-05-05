using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviourPunCallbacks
{
    public ItemInfo itemInfo;
    public GameObject itemPrefab;

    public abstract void Use();
}
