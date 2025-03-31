using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMelee : EnemyBehavior
{
    public GameObject attackHitBox;
    public EnemyAttackHitbox enemyAttackHitbox;

    bool isAttacking;
    
    public void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            if (canJump)
                CoroutineHandler.Instance.StartCoroutine(PounceAttack());
            else
                CoroutineHandler.Instance.StartCoroutine(MeleeAttack());
            hasAttacked = true;
        }
        if (!isAttacking)
            EnableClip<EnemyMelee>();
    }

    public IEnumerator PounceAttack()
    {
        isAttacking = true;
        CoroutineHandler.Instance.StartCoroutine(enemyMovement.Stop(0.7f + 5 / attackRate));
        yield return new WaitForSeconds(0.3f);
        enemyMovement.rb.AddForce(900f * SetAngle(), ForceMode2D.Force);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHitBox.SetActive(false);
        enemyAttackHitbox.hasHit = false;
        yield return new WaitForSeconds(5 / attackRate);
        isAttacking = false;
        hasAttacked = false;

    }

    public IEnumerator MeleeAttack()
    {
        isAttacking = true;
        CoroutineHandler.Instance.StartCoroutine(enemyMovement.Stop(0.35f + 5 / attackRate));
        yield return new WaitForSeconds(0.2f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        attackHitBox.SetActive(false);
        enemyAttackHitbox.hasHit = false;
        yield return new WaitForSeconds(5 / attackRate);
        isAttacking = false;
        hasAttacked = false;
    }

    public Vector2 SetAngle()
    {
        return Quaternion.Euler(0f, 0f, 20f * (enemyMovement.transform.eulerAngles.y > 0 ? -1 : 1)) * transform.right; 
    }
}
