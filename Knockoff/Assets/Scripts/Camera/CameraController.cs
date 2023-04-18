using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

namespace KnockOff.Camera
{
    public class CameraController : MonoBehaviourPun
    {
        private CinemachineVirtualCamera virtualCamera;

        private void Update()
        {
            // Only execute this code for the local player
            if (!photonView.IsMine)
            {
                return;
            }

            // Find the CinemachineVirtualCamera object in the scene
            if (virtualCamera == null)
            {
                virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            }

            PhotonView playerPhotonView = GetComponent<PhotonView>();

            // Set the virtual camera to follow the local player
            if (playerPhotonView != null)
            {
                virtualCamera.Follow = playerPhotonView.transform.GetChild(0);       //camTarget, less expensive than findbytag
                virtualCamera.LookAt = playerPhotonView.transform.GetChild(0);      //camTarget, less expensive than findbytag
            }
        }
    }
}
