using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDestroyed : MonoBehaviour
{
    PowerUpManager powerUpManager;
    public GameObject SpeedPoweUp;
    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }
    private void OnDestroy()
    {
        powerUpManager.SpeedField = SpeedPoweUp;
        powerUpManager.RespawnSpeed(SpeedPoweUp);
    }
}
