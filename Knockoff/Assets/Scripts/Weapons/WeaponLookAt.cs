using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLookAt : MonoBehaviour
{
    public Transform Crosshair;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Crosshair);
    }
}
