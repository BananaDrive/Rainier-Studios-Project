using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehavior : Movement
{
    public Transform player;


    public void FixedUpdate()
    {
        moveDirection = player.position.x - transform.position.x > 0 ? 1 : -1;
        MovementHandle();
        SpeedLimit();
    }
}
