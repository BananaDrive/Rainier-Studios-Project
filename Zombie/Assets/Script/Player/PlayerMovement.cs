using UnityEngine;

public class PlayerMovement : Movement
{
    public void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
    }

    public void FixedUpdate()
    {
        MovementHandle();
        SpeedLimit();
    }
}
