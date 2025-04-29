using UnityEngine;

public class MilitaryGeneral : EnemyBehavior
{
    public GameObject attackHitbox;
    public GameObject turret;
    public Transform anchorPoint, preRangedHitbox, rangedHitbox;
    public Transform turretSpawns;

    public float rangedCD, grenadeCD, summonCD;
    bool inRangedCD, inGrenadeCD, inSummonCD;
    bool isAttacking;

    public void FixedUpdate()
    {
        if (!isAttacking)
        {
            Vector3 direction = enemyMovement.player.position - transform.position;
            anchorPoint.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        }

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
                    animator.SetInteger("State", 3);
                if (Vector2.Distance(transform.position, enemyMovement.player.position) <= enemyMovement.distanceToStop)
                    animator.SetInteger("State", 2);
            }
        }
    }


    public void PreRangedAttack()
    {
        inRangedCD = true;
        isAttacking = true;
        preRangedHitbox.gameObject.SetActive(true);
    }

    public void RangedAttack()
    {
        rangedHitbox.gameObject.SetActive(true);
    }

    public void PostRangedAttack()
    {
        rangedHitbox.gameObject.SetActive(false);
        preRangedHitbox.gameObject.SetActive(false);
        Invoke(nameof(RangedCD), rangedCD);
        isAttacking = false;
    }


    void RangedCD() => inRangedCD = false;
    void SummonCD() => inSummonCD = false;
    void GrenadeCD() => inGrenadeCD = false;

    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}
