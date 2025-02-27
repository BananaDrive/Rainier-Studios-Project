using UnityEngine;

public class Movement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [Header("Stats")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;

    public float moveDirection;

    public void MovementHandle()
    {
        rb.AddForce(moveDirection * moveSpeed * acceleration * transform.right, ForceMode2D.Force);

        sprite.flipX = moveDirection > 0 ? false : (moveDirection < 0 ? true : sprite.flipX); //flips the player's sprite based on their directional speed

        if (moveDirection == 0)
            rb.linearVelocityX *= (100 - deceleration) / 100; //decelerates the player when not moving
    }


    public void SpeedLimit()
    {
        Vector2 flatVel = new(rb.linearVelocityX, 0);

        if (Mathf.Abs(flatVel.magnitude) > moveSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new(limitedVel.x ,rb.linearVelocityY);
        }
    }
}
