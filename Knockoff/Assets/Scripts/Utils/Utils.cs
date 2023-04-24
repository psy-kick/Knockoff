using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-20, 20), 4, Random.Range(-20, 20));
    }

    /// <summary>
    /// Finds child Transform with tag from root Transform.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static Transform FindWithTag(Transform root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag)) return t;
        }
        return null;
    }

    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    // Get the GameObject for a specific player
    public static GameObject GetPlayerGameObject(Player player)
    {
        PhotonView[] photonViews = GameObject.FindObjectsOfType<PhotonView>();

        foreach (PhotonView photonView in photonViews)
        {
            if (photonView.Owner == player)
            {
                return photonView.gameObject;
            }
        }
        return null;
    }
}
