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
        
        moveDirection = 0;
        
        if (Mathf.Abs(aiming.aimDirection) <= 0.2f)
            moveDirection = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveDirection) >= aimDistance)
            animator.SetInteger("State", 1);

        if ((Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Jump();
            jump.Play();
        }

        if ((Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Space)) && rb.linearVelocityY > 0f)
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
