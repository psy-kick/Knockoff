using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    public override void Use()
    {
        Debug.Log($"Using gun {itemInfo.ItemName}");
    }
}
