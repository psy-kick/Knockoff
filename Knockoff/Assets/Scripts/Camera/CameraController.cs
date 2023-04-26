using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using KnockOff.Player;

namespace KnockOff.Camera
{
    public class CameraController : MonoBehaviourPun
    {
        private GameObject virtualCamera;
        private GameObject aimVirtualCam;
        [SerializeField] private float normalSensitivity;
        [SerializeField] private float aimSensitivity;

        private PlayerMovement playerMovement;
        private PhotonView playerPhotonView;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            playerPhotonView = GetComponent<PhotonView>();
        }
        
        private void Update()
        {
            // Only execute this code for the local player
            if (!photonView.IsMine)
            {
                return;
            }

            // Find the CinemachineVirtualCamera object in the scene
            VirtualCam(playerPhotonView);
            AimCam(playerPhotonView);
        }

        private void AimCam(PhotonView playerPhotonView)
        {
            if (aimVirtualCam == null)
            {
                aimVirtualCam = GameObject.FindGameObjectWithTag("aimCam");
            }
            // Set the virtual camera to follow the local player
            if (playerPhotonView != null)
            {
                aimVirtualCam.GetComponent<CinemachineVirtualCamera>().Follow = playerPhotonView.transform.GetChild(0);//camTarget, less expensive than findbytag                                                                                            
            }
            if (Input.GetMouseButton(1))
            {
                aimVirtualCam.gameObject.SetActive(true);
                playerMovement.SetSensitivity(aimSensitivity);
            }
            else
            {
                aimVirtualCam.gameObject.SetActive(false);
                playerMovement.SetSensitivity(normalSensitivity);
            }
        }

        private void VirtualCam(PhotonView playerPhotonView)
        {
            if (virtualCamera == null)
            {
                virtualCamera = GameObject.FindGameObjectWithTag("3dCam");
            }
            // Set the virtual camera to follow the local player
            if (playerPhotonView != null)
            {
                virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerPhotonView.transform.GetChild(0);       //camTarget, less expensive than findbytag
            }
        }
    }
}
