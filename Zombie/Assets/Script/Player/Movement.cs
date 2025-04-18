using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Collider2D _collider;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public LayerMask ground;

    [Header("Stats")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;
    public float jumpPower;
    public float groundDistance;

    public float moveDirection;
    public float moveSpeedBuff = 1;
    public bool Grounded
    {
        get
        {
            var s = Physics2D.Raycast(transform.position, Vector2.down, _collider.bounds.extents.y + groundDistance, ground);
            return s;
        }
    }
    public bool canMove;
    public bool hasJumped;

    public void MovementHandle()
    {
        if (moveDirection == 0)
            rb.linearVelocityX *= (100 - deceleration) / 100; //decelerates the player when not moving
            
        if (!canMove)
            return;
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            moveDirection > 0 ? 0f : (moveDirection < 0 ? 180f : transform.eulerAngles.y),
            transform.eulerAngles.z
        );
    
        rb.AddForce((moveDirection == 0f ? 0 : moveSpeed * moveSpeedBuff * acceleration) * transform.right, ForceMode2D.Force);
    }


    public void SpeedLimit()
    {
        if (!canMove)
            return;
        Vector2 flatVel = new(rb.linearVelocityX, 0);

        if (Mathf.Abs(flatVel.magnitude) > moveSpeed * moveSpeedBuff)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed * moveSpeedBuff;
            rb.linearVelocity = new(limitedVel.x, rb.linearVelocityY);
        }
    }

    public void Gravity()
    {
        if (rb == null)
            return;
        rb.AddForce(1.3f * rb.mass * Physics.gravity);
    }

    public void Jump()
    {
        if (!canMove)
            return;
        rb.AddForceY(jumpPower * 10, ForceMode2D.Force);
    }

    public void UpdateStats()
    {
        
    }
}
