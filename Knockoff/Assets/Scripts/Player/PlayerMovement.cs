using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

namespace KnockOff.Player
{
    public class PlayerMovement : MonoBehaviourPunCallbacks
    {
        [Header("Player Settings")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 5f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 8f;

        [Tooltip("The height the player can jump")]
        public float JumpImpulse = 5f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Tooltip("Character Rotation Speed")]
        public float RotationSpeed = 5f;

        [Tooltip("Specify ground layer mask for detection of IsGrounded")]
        public LayerMask groundLayer;


        [Header("Camera Settings")]
        [Tooltip("The speed at which the camera rotates around the character")]
        public float sensitivity = 5f;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 60f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -20f;

        [Space(10)]

        public Transform camTarget;
        private float maxVelocityChange = 10f;
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;


        private Rigidbody rb;
        [SerializeField] private float mouseSensitivity = 100f;
        private Vector2 _rotation = Vector2.zero;

        private bool isSprinting;       //will be handled through stamina

        public bool isGrounded { get; set; }    //networked

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

            _rotation.y -= mouseY;
            _rotation.x += mouseX;
            _rotation.y = Mathf.Clamp(_rotation.y, BottomClamp, TopClamp);

            Quaternion rot = Quaternion.Euler(0f, _rotation.x, 0f);
            Quaternion camRot = Quaternion.Euler(_rotation.y, _rotation.x, 0f);

            transform.rotation = rot;
            camTarget.rotation = camRot;
        }

        public void Move()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                isSprinting = true;
            else
                isSprinting = false;

            float speed = isSprinting ? SprintSpeed : MoveSpeed;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;

            Vector3 moveDirection = transform.rotation * (Vector3.right * movement.x + Vector3.forward * movement.z);

            // Apply the movement vector to the Rigidbody's velocity
            rb.velocity = moveDirection * MoveSpeed + new Vector3(0f, rb.velocity.y, 0f);
        }


        private void Update()
        {
            if (photonView.IsMine)
            {
                Look();
                Move();
                Jump();
            }
        }

        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * JumpImpulse, ForceMode.Impulse);
            }
        }


        private void OnCollisionEnter(Collision collision)
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

