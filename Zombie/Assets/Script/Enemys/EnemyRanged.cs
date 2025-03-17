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
        bullet.GetComponent<Transform>().position = transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce((enemyMovement.sprite.flipX ? -1f : 1f) * 10f * 20f * transform.right, ForceMode2D.Force);
    }

    public IEnumerator BurstFire()
    {
        for (int i = 0; i < burstFireAmount; i++)
        {
            RangedAttack();
            yield return new WaitForSeconds(attackRate / 6);
        }
        StartCoroutine(AttackCD());
    }
}
