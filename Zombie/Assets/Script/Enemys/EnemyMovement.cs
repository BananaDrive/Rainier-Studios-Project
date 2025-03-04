using NUnit.Framework.Interfaces;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Transform player;
    public LayerMask playerLayer;

    public float detectRadius;

    public void FixedUpdate()
    {
        if (canMove)
            MovementHandle();
        PlayerDetection();
        SpeedLimit();
    }

    public void PlayerDetection()
    {
        player = null;

        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, detectRadius, playerLayer))
        {
            player = collider.GetComponent<Transform>();
        }

        if (player != null)
            moveDirection = player.position.x - transform.position.x > 0 ? 1 : -1;
    }
}
