using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // This script should to be attached to main camera, then link the player body to it as reference.
    [Header("Mouse settings (Main Camera)")]
    [Tooltip("Mouse sensitivity")]
    [SerializeField] private float mouseSensitivity = 100f;
    [Tooltip("Transform player")]
    [SerializeField] private Transform playerBody;  // Firstpersonplayer
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 55f); 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
