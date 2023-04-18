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

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

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
        {/*
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
            */
        }

        #endregion

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }
    }

}