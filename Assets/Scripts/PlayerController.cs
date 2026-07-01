using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private Transform cameraTransform;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
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
        //Quaternion.LookRotation(cameraForward, cameraRight, )
        if (crmd.sqrMagnitude > .001f)
        {
            //LOOK ROTATION and SLERP
            Quaternion targetRotation = Quaternion.LookRotation(crmd);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        //Vector3 moveDirection = new(moveX, 0f, moveZ);
        Vector3 finalMove = crmd * moveSpeed;

        cc.Move(finalMove * Time.deltaTime);
    }
}
