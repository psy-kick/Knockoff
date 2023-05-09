using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviourPunCallbacks
{
    public GameObject SpeedobjectToSpawn;
    public GameObject EnergyobjectToSpawn;
    public int NumberOfSpeedObjectsToSpawn;
    public int NumberOfEnergyObjectsToSpawn;

    public GameObject SpeedField;

    [SerializeField]
    private GameObject[] SpeedspawnPoints;
    [SerializeField]
    private GameObject[] EnergyspawnPoints;

    void Start()
    {
        SpeedspawnPoints = GameObject.FindGameObjectsWithTag("ZoomSpawn");
        EnergyspawnPoints = GameObject.FindGameObjectsWithTag("EnergySpawn");
        SpeedSpawnObjects();
        EnergySpawnObjects();
    }
    void SpeedSpawnObjects()
    {
        for (int i = 0; i < NumberOfSpeedObjectsToSpawn; i++)
        {
            int randomSpawnIndex = Random.Range(0, SpeedspawnPoints.Length);
            Vector3 spawnPosition = SpeedspawnPoints[randomSpawnIndex].transform.position;
            Quaternion spawnRotation = SpeedspawnPoints[randomSpawnIndex].transform.rotation;

            PhotonNetwork.Instantiate(SpeedobjectToSpawn.name, spawnPosition, spawnRotation);
        }
    }
    void EnergySpawnObjects()
    {
        for (int i = 0; i < NumberOfEnergyObjectsToSpawn; i++)
        {
            int randomSpawnIndex = Random.Range(0, EnergyspawnPoints.Length);
            Vector3 spawnPosition = EnergyspawnPoints[randomSpawnIndex].transform.position;
            Quaternion spawnRotation = EnergyspawnPoints[randomSpawnIndex].transform.rotation;

            PhotonNetwork.Instantiate(EnergyobjectToSpawn.name, spawnPosition, spawnRotation);
        }
    }
    public IEnumerator ReSpawnSpeedObject(GameObject _speedObject)
    {
        yield return new WaitForSeconds(6f);
        int randomSpawnIndex = Random.Range(0, SpeedspawnPoints.Length);
        Vector3 spawnPosition = SpeedspawnPoints[randomSpawnIndex].transform.position;
        Quaternion spawnRotation = SpeedspawnPoints[randomSpawnIndex].transform.rotation;

        PhotonNetwork.Instantiate(_speedObject.name, spawnPosition, spawnRotation);
    }
    public IEnumerator ReSpawnEnergyObject(GameObject _energyObject)
    {
        yield return new WaitForSeconds(6f);
        int randomSpawnIndex = Random.Range(0, EnergyspawnPoints.Length);
        Vector3 spawnPosition = EnergyspawnPoints[randomSpawnIndex].transform.position;
        Quaternion spawnRotation = EnergyspawnPoints[randomSpawnIndex].transform.rotation;

        PhotonNetwork.Instantiate(_energyObject.name, spawnPosition, spawnRotation);
    }
    public void RespawnSpeed(GameObject _speedObject)
    {
        StartCoroutine(ReSpawnSpeedObject(SpeedField));
    }
}
