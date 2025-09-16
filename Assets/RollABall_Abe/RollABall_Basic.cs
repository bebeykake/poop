using UnityEngine;
using UnityEngine.InputSystem;

public class RollABall_Basic : MonoBehaviour
{

    public float acceleration = 10f;

    Rigidbody rb;
    Vector2 moveInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // This will be called automatically by PlayerInput (UnityEvent)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 force = new Vector3(moveInput.x, 0f, moveInput.y) * acceleration;
        rb.AddForce(force, ForceMode.Acceleration);
    }
}
