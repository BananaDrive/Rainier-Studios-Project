using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyBehavior
{
    public GameObject attackHitBox;
    public EnemyAttackHitbox enemyAttackHitbox;

    public bool canPounce;
    bool isAttacking;

    public void FixedUpdate()
    {
        animator.SetInteger("State", 0);

        if (enemyMovement.moveDirection != 0)
            animator.SetInteger("State", 1);
        
        if (enemyMovement.player != null)
        {
            if (Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
                animator.SetInteger("State", 2);
        }

        if (!hasAttacked)
            EnableClip<EnemyMelee>();
    }

    public void Pounce()
    {
        if (canPounce)
            enemyMovement.rb.AddForce(900f * SetAngle(), ForceMode2D.Force);
    }
    public void EnableHitbox() => attackHitBox.SetActive(true);
    public void DisableHitbox() => attackHitBox.SetActive(false);
    public Vector2 SetAngle() => Quaternion.Euler(0f, 0f, 20f * (enemyMovement.transform.eulerAngles.y > 0 ? -1 : 1)) * transform.right; 
}
