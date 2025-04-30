using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    public AudioSource jump;
    public void Update()
    {
        var gamepad = Gamepad.current;
        
        moveDirection = Input.GetAxisRaw("Horizontal");

        if ((gamepad.buttonSouth.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Jump();
            jump.Play();
        }

        if ((gamepad.buttonSouth.wasReleasedThisFrame || Input.GetKeyUp(KeyCode.Space)) && rb.linearVelocityY > 0f)
        {
            rb.linearVelocityY = 0f;
            rb.AddForceY(-jumpPower, ForceMode2D.Force);
        }   
    }

    public void FixedUpdate()
    {
        Gravity();
        MovementHandle();
        SpeedLimit();
    }
}
