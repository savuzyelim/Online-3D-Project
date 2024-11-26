using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float walkSpeed = 8f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;
    [Space]
    public float jumpHeight = 5f;
    public float airControl = 0.5f;

    private Vector2 input;
    private Rigidbody rb;
    private bool sprinting;
    private bool jumping;
    private bool grounded;

    private PhotonView _photonView;
    private Animator animator;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (!_photonView.IsMine)
        {
            // Yerel olmayan oyuncularýn scriptlerini ve kameralarýný devre dýþý býrak
            Destroy(this);
            Camera playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return; // Yerel olmayan oyuncular için güncelleme yapma
        }

        // Kullanýcý girdilerini al
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("Sprint");
        jumping = Input.GetButton("Jump");

        // Animasyon parametrelerini güncelle
        UpdateAnimations();
    }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine)
        {
            return; // Yerel olmayan oyuncular için fizik güncellemeleri yapma
        }

        if (grounded)
        {
            if (jumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
            float speed = sprinting ? sprintSpeed : walkSpeed;
            Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Hareket giriþi yokken sürtünme uygulama
            if (input.magnitude == 0)
            {
                rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
            }
        }
        else
        {
            float speed = sprinting ? sprintSpeed * airControl : walkSpeed * airControl;
            Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Hareket giriþi yokken sürtünme uygulama
            if (input.magnitude == 0)
            {
                rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
            }
        }

        grounded = false;
    }

    private void UpdateAnimations()
    {
        bool isMoving = input.magnitude > 0;
        bool isRunning = isMoving && sprinting;

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
    }
}
