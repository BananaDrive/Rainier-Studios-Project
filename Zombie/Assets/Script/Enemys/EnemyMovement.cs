using System.Collections;
using UnityEngine;

public class EnemyMovement : Movement
{
    [Header("Enemy Stuff")]
    public EnemyBehavior enemyBehavior;
    public Transform player;

    [Header("Detect Config")]
    public LayerMask playerLayer;
    public float distanceToStop;
    public float detectRadius;

    [Header("Wander Config")]
    public float wanderSpeedMulti;
    public float wanderDirection;
    public float maxWanderSpeedMulti;
    public float wanderRate;
    public bool canWander;
    bool hasWandered;

    float originalSpeed;

    public void Start()
    {
        originalSpeed = moveSpeed;
        moveSpeedBuff = 1f;
    }

    public void FixedUpdate()
    {
        moveSpeed = originalSpeed;
        moveDirection = 0;
        Gravity();
        Detection();
        WanderAround();
        MovementHandle();
        JumpCheck();
        SpeedLimit();
    }

    public void WanderAround()
    {
        if (player == null)
        {
            if (!hasWandered && canWander)
            {
                hasWandered = true;
                StartCoroutine(Wander());
            }
            moveSpeed *= wanderSpeedMulti;
            moveDirection = wanderDirection;
        }
    }

    public void Detection()
    {
        player = null;

        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, detectRadius, playerLayer))
        {
            player = collider.GetComponent<Transform>();
        }

        if (player != null)
        {
            if (Vector2.Distance(player.position, transform.position) >= distanceToStop && canMove)
                moveDirection = player.position.x - transform.position.x > 0 ? 1 : -1;
            transform.rotation = Quaternion.Euler(0f, player.position.x - transform.position.x > 0 ? 0f : 180f, 0f);
        }
    }

    public void JumpCheck()
    {
        Debug.DrawRay(transform.position, new Vector2(transform.right.x, -1.5f));
        if ((Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - _collider.bounds.extents.y + 0.2f), transform.right, _collider.bounds.extents.x + 0.3f, ground) || 
        !Physics2D.Raycast(transform.position, new Vector2(transform.right.x, -1.5f), 2f, ground)) &&
        Grounded)
        {
            Jump();
        }
    }
    
    public IEnumerator Wander()
    {
        wanderSpeedMulti = Random.Range(maxWanderSpeedMulti / 2f, maxWanderSpeedMulti);
        wanderDirection = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(Random.Range(wanderRate * 0.75f, wanderRate * 1.25f));
        hasWandered = false;
    }

    public IEnumerator Stop(float time)
    {
        moveDirection = 0f;
        canMove = false;
        rb.linearVelocityX = 0f;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
