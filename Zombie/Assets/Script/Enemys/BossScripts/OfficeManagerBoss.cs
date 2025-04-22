using System.Collections;
using UnityEngine;

public class OfficeManagerBoss : EnemyBehavior
{
    public GameObject attackHitbox;
    public GameObject printer;
    public SpriteRenderer spr;

    public float attackStartUp, hitboxDuration;

    public float slamCDLength;
    float slamCDDuration;
    bool slamCD;

    public void Start()
    {
        slamCD = true;
    }

    public void FixedUpdate()
    {    
        AttackCheck();
    }

    public void AttackCheck()
    {
        if (enemyMovement.player != null)
        {
            float distance = Vector2.Distance(transform.position, enemyMovement.player.position);
            if (!hasAttacked)
            {
                hasAttacked = true;
                if (!slamCD)
                {
                    CoroutineHandler.Instance.StartCoroutine(SlamAttack());
                    return;
                }
                if (distance > enemyMovement.distanceToStop)
                {
                    CoroutineHandler.Instance.StartCoroutine(ThrowAttack(distance * 0.9f));
                }
                else
                {
                    CoroutineHandler.Instance.StartCoroutine(MeleeAttack());
                }
            }

            if (slamCD)
                Cooldown();
        }
    }

    public void Cooldown()
    {
        slamCDDuration += Time.deltaTime;
        if (slamCDDuration >= slamCDLength)
        {
            slamCDDuration = 0;
            slamCD = false;
        } 
    }

    public IEnumerator ThrowAttack(float throwPower)
    {
        CoroutineHandler.Instance.StartCoroutine(enemyMovement.Stop(attackRate / 3));
        spr.color = Color.red;
        yield return new WaitForSeconds(attackStartUp);
        spr.color = Color.white;

        GameObject thrownObj = Instantiate(printer);
        Vector2 throwDirection = (enemyMovement.player.position - transform.position).normalized;
        throwDirection.y += 1f / Mathf.Clamp(throwPower, 1f, 10f);
        thrownObj.transform.position = transform.position;
        thrownObj.GetComponent<Rigidbody2D>().AddForce(110f * throwPower * throwDirection, ForceMode2D.Force);

        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }

    public IEnumerator MeleeAttack()
    {
        CoroutineHandler.Instance.StartCoroutine(enemyMovement.Stop(attackRate / 3));
        spr.color = Color.red;
        yield return new WaitForSeconds(attackStartUp);
        spr.color = Color.white;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }

    public IEnumerator SlamAttack()
    {
        slamCD = true;
        CoroutineHandler.Instance.StartCoroutine(enemyMovement.Stop(2.5f));
        spr.color = Color.blue;
        yield return new WaitForSeconds(attackStartUp * 4);
        spr.color = Color.white;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }
}
