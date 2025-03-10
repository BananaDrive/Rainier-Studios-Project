using UnityEngine;

public class PlayerMovement : Movement
{
    public void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0f)
        {
            rb.linearVelocityY = 0f;
            rb.AddForceY(-jumpPower, ForceMode2D.Force);
        }
            
    }

    public void FixedUpdate()
    {
        MovementHandle();
        SpeedLimit();
    }
}
