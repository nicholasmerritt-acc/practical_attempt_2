using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cameraTransform;

    [Header("Jumping")]
    private bool hasJumped = false;
    private float gravity = -20f;
    private float verticalVelocity = 0f;
    [SerializeField] private float jumpVelocity = 9f;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //CHECK FALLING and set gravity and velocity
        bool falling = (verticalVelocity < 0f) && hasJumped;
        float gravityThisFrame = gravity;
        if (falling)
        {
            gravityThisFrame *= 5f;
        }
        verticalVelocity += gravity * Time.deltaTime;

        //CHECK GROUNDED
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpVelocity;
                Debug.Log("jump");
                hasJumped = true;
            }
        }
        Vector3 upVelocity = verticalVelocity * Vector3.up;


        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        //zero the ys and normalize
        cameraRight.y = 0f;
        cameraForward.y = 0f;
        cameraRight.Normalize();
        cameraForward.Normalize();

        //ASSEMBLE
        Vector3 crmd = (cameraRight * moveX + cameraForward * moveZ).normalized;

        //calc camera directed move direction
        if (crmd.sqrMagnitude > .001f)
        {
            //LOOK ROTATION and SLERP
            Quaternion targetRotation = Quaternion.LookRotation(crmd);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 finalMove = crmd * moveSpeed;
        characterController.Move((finalMove + upVelocity) * Time.deltaTime);
    }
}
