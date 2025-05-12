using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRanged : EnemyBehavior
{
    public GameObject bullet;
    public Transform shotPoint;
    public int burstFireAmount;
    int bulletPoolIndex;

    void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
    }
    void FixedUpdate()
    {
        animator.SetInteger("State", 0);

        if (enemyMovement.moveDirection != 0)
            animator.SetInteger("State", 1);
        
        if (enemyMovement.player != null)
        {
            if (Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
            {
                Vector3 direction = enemyMovement.player.position - transform.position;
                shotPoint.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
                animator.SetInteger("State", 2);
            }
        }
        if (!hasAttacked)
            EnableClip<EnemyRanged>();
    }
            
    public void Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletPoolIndex);

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.layerToHit = enemyMovement.playerLayer;

        StartCoroutine(bulletScript.Despawn());
        bullet.GetComponent<Transform>().SetPositionAndRotation(shotPoint.position, shotPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(40f * shotPoint.transform.right, ForceMode2D.Force);
    }

    public IEnumerator BurstFire()
    {
        if (!interrupted)
        {
            for (int i = 0; i < burstFireAmount; i++)
            {
                Shoot();
                yield return new WaitForSeconds(attackRate / 6);
            }
        }
    }
}
