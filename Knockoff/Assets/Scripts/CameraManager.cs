using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnockOff.Camera
{
    public class CameraManager : MonoBehaviourPunCallbacks
    {
        private Cinemachine.CinemachineVirtualCamera vcam;

        [SerializeField] private Transform camTarget;

        private void Awake()
        {
            if (!photonView.IsMine)
                return;

            vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                vcam.m_Follow = camTarget;
                vcam.m_LookAt = camTarget;
            }
        }

    }
}
