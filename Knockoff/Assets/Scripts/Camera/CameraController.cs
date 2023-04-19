using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

namespace KnockOff.Camera
{
    public class CameraController : MonoBehaviourPun
    {
        private GameObject virtualCamera;

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
                virtualCamera = GameObject.FindGameObjectWithTag("3dCam");
            }

            PhotonView playerPhotonView = GetComponent<PhotonView>();
            // Set the virtual camera to follow the local player
            if (playerPhotonView != null)
            {
                virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerPhotonView.transform.GetChild(0);       //camTarget, less expensive than findbytag
            }
        }
    }
}
