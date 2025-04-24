using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class OfficeManagerBoss : EnemyBehavior
{
    public GameObject printer;

    public float rangedCD;
    float throwPower;
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
                if (Vector2.Distance(transform.position, enemyMovement.player.position) <= enemyMovement.distanceToStop)
                    animator.SetInteger("State", 2);
                else if (!inRangedCD)
                {
                    throwPower = 4.5f + Vector2.Distance(transform.position, enemyMovement.player.position) / 3.4f;
                    animator.SetInteger("State", 3);
                }
            }
        }
    }

    public void ThrowAttack()
    {
        inRangedCD = true;
        GameObject thrownObj = Instantiate(printer);
        Vector2 throwDirection = (enemyMovement.player.position - transform.position).normalized;
        throwDirection.y += 0.75f;
        thrownObj.transform.position = transform.position;
        thrownObj.GetComponent<Rigidbody2D>().AddForce(55f * throwPower * throwDirection, ForceMode2D.Force);
        Invoke(nameof(RangedCD), rangedCD);
    }

    void RangedCD() => inRangedCD = false;
}
