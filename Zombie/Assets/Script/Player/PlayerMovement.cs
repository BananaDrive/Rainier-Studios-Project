using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    public Animator animator;
    public float aimDistance;
    public AudioSource jump;
    public Aiming aiming;
    
    public void Update()
    {
        animator.SetInteger("State", 0);

        var gamepad = Gamepad.current;
        bool controller = false;
        if (gamepad != null)
            controller = true;
        
        moveDirection = 0;
        
        if (Mathf.Abs(aiming.aimDirection) <= 0.2f)
            moveDirection = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveDirection) >= aimDistance)
            animator.SetInteger("State", 1);

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
        Movement();
        SpeedLimit();
    }

    void Movement()
    {
        if (Mathf.Abs(moveDirection) < aimDistance)
        {
            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                moveDirection > 0 ? 0f : (moveDirection < 0 ? 180f : transform.eulerAngles.y),
                transform.eulerAngles.z
            );
            moveDirection = 0;
        }
        MovementHandle();
    }
}
