using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;


    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
