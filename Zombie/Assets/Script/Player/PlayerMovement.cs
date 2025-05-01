using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    public AudioSource jump;
    public void Update()
    {
        var gamepad = Gamepad.current;
        bool controller = false;
        if (gamepad != null)
            controller = true;
        
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (((controller && gamepad.buttonSouth.wasPressedThisFrame) || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Jump();
            jump.Play();
        }

        if (((controller && gamepad.buttonSouth.wasPressedThisFrame) || Input.GetKeyUp(KeyCode.Space)) && rb.linearVelocityY > 0f)
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
