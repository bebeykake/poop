using UnityEngine;
using UnityEngine.InputSystem;

//This script uses Unity's new I put system to rotate the player left to right, and rotate the camera up and down
public class FirstPersonLook : MonoBehaviour
{

    [Header("Mouse Sensitivity Settings")]
    public float sensitivityX = 1.0f; //Horizontal mouse sensitivity
    public float sensitivityY = 1.0f; //Vertical mouse sensitivity

    [Header("Camera Setup")]
    public Transform cameraRoot; //Reference to the empty object (CameraRoot) that is referenced by Cinemachine camera

    // Internal state to track accumulated look angles
    private float yaw;//Horizontal rotation (Y axis, rotates the player)
    private float pitch;//Vertical rotation (X axis, rotates the camera's pivot)

    // Lock the cursor when the game starts
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Called automatically by Input System when look input (like the mouse) is received
    //uses delta (change in mouse position) to adjust yaw and pitch.
    public void OnLook(InputAction.CallbackContext context)
    {
        //Gert the movement delta from the input (mouse or stick)
        Vector2 delta = context.ReadValue<Vector2>();

        // Adjust yaw (horizontal) and apply it directly to the player's Y-axis rotation
        yaw = delta.x * sensitivityX;
        transform.Rotate(0f, yaw, 0f);

        // Accumulate and clamp pitch
        pitch -= delta.y * sensitivityY;
        pitch = Mathf.Clamp(pitch, -60f, 60f); //the camera can't spin more or less than 60 degrees up or down, adjust if necessary

        // Apply pitch
        cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
