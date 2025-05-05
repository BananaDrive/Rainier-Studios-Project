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
            var s = Physics2D.CircleCast(transform.position, _collider.bounds.extents.x - 0.05f, Vector2.down, _collider.bounds.extents.y + groundDistance, ground);
            return s;
        }
    }
    public bool canMove;
    public bool hasJumped;

    public void MovementHandle()
    {       
        if (!canMove)
            return;
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            moveDirection > 0 ? 0f : (moveDirection < 0 ? 180f : transform.eulerAngles.y),
            transform.eulerAngles.z
        );
    
        rb.AddForce((moveDirection == 0f ? 0 : moveSpeed * Mathf.Abs(moveDirection) * moveSpeedBuff * acceleration) * transform.right, ForceMode2D.Force);
    }


    public void SpeedLimit()
    {
        if (!canMove)
            return;
        Vector2 flatVel = new(rb.linearVelocityX, 0);

        if (Mathf.Abs(flatVel.magnitude) > moveSpeed * moveSpeedBuff * Mathf.Abs(moveDirection))
        {
            rb.linearVelocityX *= (100 - deceleration) / 100;
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
