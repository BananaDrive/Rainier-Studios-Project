using System.Collections;
using UnityEngine;

public class EnemyMovement : Movement
{
    [Header("Enemy Stuff")]
    public EnemyBehavior enemyBehavior;
    public Transform player;
    public LayerMask playerLayer;

    public float distanceToStop;
    public float detectRadius;

    bool hasWandered;

    public void Start()
    {
        moveSpeed = enemyBehavior.speed;
    }

    public void FixedUpdate()
    {
        Gravity();
        if (canMove)
            MovementHandle();
        PlayerDetection();

        if (player == null)
        {
            moveSpeedBuff = -50f;

            if (hasWandered == false)
            {
                hasWandered = true;
                StartCoroutine(Wander());
            }
        }
        SpeedLimit();
    }

    public void PlayerDetection()
    {
        player = null;

        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, detectRadius, playerLayer))
        {
            player = collider.GetComponent<Transform>();
        }

        if (player != null && Vector2.Distance(player.position, transform.position) > distanceToStop)
        {
            moveDirection = player.position.x - transform.position.x > 0 ? 1 : -1;
            hasWandered = false;
        }
    }
    
    public IEnumerator Wander()
    {
        moveDirection = Random.Range(1f, -1f);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        moveDirection = 0;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        hasWandered = false;
    }
}
