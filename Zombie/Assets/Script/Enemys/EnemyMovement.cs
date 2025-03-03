using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Transform player;

    public void FixedUpdate()
    {
        moveDirection = player.position.x - transform.position.x > 0 ? 1 : -1;
        MovementHandle();
        SpeedLimit();
    }
}
