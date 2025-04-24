using System.Collections.Generic;
using UnityEngine;

public class OfficeManagerBoss : EnemyBehavior
{
    public GameObject attackHitBox;
    public GameObject printer;
    public Transform printerSpawns, zombieSpawns;

    public float rangedCD, slamCD, summonCD;
    bool inRangedCD, inSlamCD, inSummonCD;
    float throwPower;


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
                if (!inSummonCD)
                {
                    animator.SetInteger("State", 5);
                    return;
                }
                if (!inSlamCD)
                {
                    animator.SetInteger("State", 4);
                    return;
                }
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

    public void SlamAttack()
    {
        inSlamCD = true;
        for (int i = 0; i < printerSpawns.childCount; i++)
        {
            GameObject printerObj = Instantiate(printer);
            printerObj.transform.position = printerSpawns.GetChild(i).position;
        }
        Invoke(nameof(SlamCD), slamCD);
    }

    public void SpawnZombies()
    {
        inSummonCD = true;
        for (int i = 0; i < zombieSpawns.childCount; i++)
        {
            zombieSpawns.GetChild(i).gameObject.SetActive(true);
        }
        Invoke(nameof(SummonCD), summonCD);
    }

    void RangedCD() => inRangedCD = false;
    void SlamCD() => inSlamCD = false;
    void SummonCD() => inSummonCD = false;

    public void EnableHitbox() => attackHitBox.SetActive(true);
    public void DisableHitbox() => attackHitBox.SetActive(false);
}
