using Cinemachine;
using KnockOff.Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSShooter : MonoBehaviourPun
{
    private GameObject aimVirtualCam;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (aimVirtualCam == null)
        {
            aimVirtualCam = GameObject.FindGameObjectWithTag("aimCam");
        }

        PhotonView playerPhotonView = GetComponent<PhotonView>();
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
}