using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private static List<Transform> spawnPoints = new List<Transform>();
    private static int lastUsedSpawnPoint = -1;

    public PhotonTeam team;

    private void Awake()
    {
        spawnPoints.Add(transform);
    }

    public static Transform GetSpawnPoint()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available");
            return null;
        }

        int randomIndex = lastUsedSpawnPoint;

        // Keep searching for an unused spawn point until we find one or we have checked all of them
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            randomIndex = (randomIndex + 1) % spawnPoints.Count;
            if (randomIndex != lastUsedSpawnPoint)
            {
                lastUsedSpawnPoint = randomIndex;
                return spawnPoints[randomIndex];
            }
        }

        // If we have checked all of the spawn points and none are available, just use the last one
        Debug.LogWarning("All spawn points are in use");
        return spawnPoints[lastUsedSpawnPoint];
    }
}
