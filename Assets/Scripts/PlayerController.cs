using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cameraTransform;
    private Rigidbody rb;

    [Header("Jumping")]
    private bool hasJumpBeenPressed = false;
    [SerializeField] private float jumpVelocity = 9f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveX;
    [SerializeField] private float moveZ;

    [Header("Firing")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            hasJumpBeenPressed = true;
        }

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire2"))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (projectile != null)
        {
            Instantiate(projectile, firePoint.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMove();
    }

    private void HandleMove()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        //1. zero the ys and normalize
        cameraRight.y = 0f;
        cameraForward.y = 0f;
        cameraRight.Normalize();
        cameraForward.Normalize();

        //2. ASSEMBLE
        Vector3 cameraRelatedMoveDirection = ((cameraRight * moveX) + cameraForward * moveZ).normalized;

        //calc camera directed move direction
        if (cameraRelatedMoveDirection.sqrMagnitude > .001f)
        {
            //3. LOOK ROTATION and SLERP
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelatedMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 finalMove = cameraRelatedMoveDirection * moveSpeed; // + upVelocity
        characterController.Move((finalMove) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (hasJumpBeenPressed)
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            hasJumpBeenPressed = false;
        }
    }
}
