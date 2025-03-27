using System.Collections;
using UnityEngine;

public class EnemyRanged : EnemyBehavior
{
    public float burstFireAmount;
    void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            StartCoroutine(BurstFire());
        }
        EnableClip<EnemyRanged>();
    }

    public void RangedAttack()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(0);

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.layerToHit = enemyMovement.playerLayer;

        StartCoroutine(bulletScript.Despawn());
        bullet.GetComponent<Transform>().SetPositionAndRotation(transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(40f * transform.right, ForceMode2D.Force);
    }

    public IEnumerator BurstFire()
    {
        enemyMovement.canMove = false;
        enemyMovement.rb.linearVelocityX = 0f;
        for (int i = 0; i < burstFireAmount; i++)
        {
            RangedAttack();
            yield return new WaitForSeconds(attackRate / 6);
        }
        enemyMovement.canMove = true;
        StartCoroutine(AttackCD());
    }
}
