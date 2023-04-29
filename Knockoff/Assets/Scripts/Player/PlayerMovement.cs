using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace KnockOff.Player
{
    public class PlayerMovement : MonoBehaviourPunCallbacks
    {
        [Header("Player Settings")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 8.5f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 15f;

        [Tooltip("The height the player can jump")]
        public float JumpImpulse = 9f;

        [Tooltip("The jump cooldown for multiple jumps")]
        public float JumpCooldown = 1f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;       //might be removed

        [Tooltip("Specify ground layer mask for detection of IsGrounded")]
        public LayerMask groundLayer;

        [Tooltip("rot")]
        public Transform playerRotationModel;

        [Header("Camera Settings")]
        [Tooltip("The camera target of the player")]
        public Transform camTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 60f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -20f;

        [Space(10)]

        [Header("Audio Settings")]
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        #region Private Fields
        private float mouseSensitivity;
        private Rigidbody rb;
        private Vector2 _rotation = Vector2.zero;
        private bool canJump = true;
        public float rotateSpeed = 5f;
        #endregion

        #region Public Fields
        public bool isSprinting { get; set; }       //will be handled through stamina
        public bool isGrounded { get; set; }    //networked

        private bool canRotate = true;

        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            if (!rb)
            {
                Debug.LogError("PlayerMovement is Missing Rigidbody Component", this);
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Look()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            if (canRotate)
            {
                if (Mathf.Abs(mouseX) == 0 && Mathf.Abs(mouseY) == 0)
                    _rotation = transform.rotation.eulerAngles / mouseSensitivity;

                _rotation.y -= mouseY;
                _rotation.x += mouseX;
                _rotation.y = Mathf.Clamp(_rotation.y, BottomClamp, TopClamp);

                Quaternion rot = Quaternion.Euler(0f, _rotation.x, 0f);
                Quaternion camRot = Quaternion.Euler(_rotation.y, _rotation.x, 0f);


                transform.rotation = rot;
                camTarget.rotation = camRot;
            }
        }

        public void LookM()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float rotationAmountX = mouseX * mouseSensitivity * Time.deltaTime;
            float rotationAmountY = -mouseY * mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationAmountX);
            camTarget.Rotate(Vector3.right, rotationAmountY);
        }

        public void SetRotation(Quaternion spawnRot)
        {
            canRotate = false;
            
            _rotation = spawnRot.eulerAngles / mouseSensitivity;

            Quaternion rot = Quaternion.Euler(0f, _rotation.x, 0f);
            Quaternion camRot = Quaternion.Euler(_rotation.y, _rotation.x, 0f);

            transform.rotation = rot * spawnRot;
            camTarget.rotation = camRot * spawnRot;
        }

        public void ClearManualRotation()
        {
            _rotation = Vector2.zero;
            canRotate = true;
        }

        public void Move()
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isGrounded)
                isSprinting = true;
            else
                isSprinting = false;

            float speed = isSprinting ? SprintSpeed : MoveSpeed;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;

            Vector3 moveDirection = transform.rotation * (Vector3.right * movement.x + Vector3.forward * movement.z);
            // Apply the movement vector to the Rigidbody's velocity
            rb.velocity = moveDirection * speed + new Vector3(0f, rb.velocity.y, 0f);
        }


        private void Update()
        {
            if (photonView.IsMine)
            {
                LookM();
                Move();
                Jump();
            }
        }

        public void Jump()      //need to turn it into multiple jumps with a cooldown
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                if (rb.velocity != Vector3.zero)
                    rb.velocity = Vector3.zero;

                rb.AddForce(Vector3.up * JumpImpulse, ForceMode.Impulse);
                canJump = false;
                StartCoroutine(WaitForJumpCooldown());
            }
        }
        private IEnumerator WaitForJumpCooldown()
        {
            yield return new WaitForSeconds(JumpCooldown);
            canJump = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3)
                isGrounded = true;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == 3)
                isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == 3)
                isGrounded = false;
        }

        public void SetSensitivity(float newSensitivity)
        {
            mouseSensitivity = newSensitivity;
        }

        /*
        private void OnFootstep(AnimationEvent animationEvent)
        {
          if (animationEvent.animatorClipInfo.weight > 0.5f)
          {
              if (FootstepAudioClips.Length > 0)
              {
                  var index = Random.Range(0, FootstepAudioClips.Length);
                  AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
              }
          }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
          if (animationEvent.animatorClipInfo.weight > 0.5f)
          {
              AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
          }
        }*/

    }
}

