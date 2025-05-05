using UnityEngine;

public class Farmer : EnemyBehavior
{
    public GameObject attackHitbox;

    bool inRangedCD;

    public void FixedUpdate()
    {
        animator.SetInteger("State", 0);

        if (enemyMovement.moveDirection != 0)
            animator.SetInteger("State", 1);

        AttackCheck();
    }


    public void AttackCheck()
    {
        if (enemyMovement.player != null)
        {
            if (!hasAttacked)
            {
                if (!inRangedCD)
                {
                    animator.SetInteger("State", 3);
                }
                if (Vector2.Distance(transform.position, enemyMovement.player.position) <= enemyMovement.distanceToStop)
                    animator.SetInteger("State", 2);
            }
        }
    }

    public void DashAttack(Vector2 lungeAngle)
    {
        enemyMovement.rb.AddForce(900f * lungeAngle, ForceMode2D.Force);
    }


    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}
