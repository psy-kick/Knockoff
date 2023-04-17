using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KnockOff.Player
{
    public class PlayerAnimatorManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields 

        private Animator anim;

        [SerializeField] private float directionDampTime = 0.25f;

        #endregion


        #region Monobehaviour Callbacks


        private void Start()
        {
            anim = GetComponent<Animator>();

            if (!anim)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }


        private void Update()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected == true)
                return;

            if (!anim)
                return;

            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Jump");
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0)
                v = 0;

            anim.SetFloat("Speed", h * h + v * v);
            anim.SetFloat("Direction", h, directionDampTime, Time.deltaTime);

        }

        #endregion
    }

}