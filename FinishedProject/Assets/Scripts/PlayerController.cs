using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity for mouse look
    public float moveSpeed = 5f; // Movement speed of the player

    public Transform playerBody; // Reference to the player body (capsule)
    public Camera playerCamera; // Reference to the camera
    private CharacterController characterController; // Reference to the character controller

    private float xRotation = 0f; // Rotation for up/down movement

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Mouse Look (up/down and left/right)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the up/down rotation

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply up/down rotation
        playerBody.Rotate(Vector3.up * mouseX); // Apply left/right rotation to player body

        // Player Movement (forward/backward, left/right)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move); // Move the character using CharacterController
    }
}
