using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private Vector3 offset = new(0f, 1.5f, 0f);

    [SerializeField] private float yaw = 0f;
    [SerializeField] private float pitch = 0f;
    [SerializeField] private float sensitivityX = 5f;
    [SerializeField] private float sensitivityY = 5f;

    [SerializeField] private float maxPitch = 50f;
    [SerializeField] private float minPitch = -20f;

    [SerializeField] private float distance = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //CURSOR:
        //lock state
        //visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //INPUT

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        yaw += mouseX * sensitivityX;
        pitch -= mouseY * sensitivityY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        //ROTATE/SWAP
        Vector3 focalPoint = playerTransform.position + offset;
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        //f - RFD
        transform.position = focalPoint - targetRotation * Vector3.forward * distance;
        transform.rotation = targetRotation;
    }
}
