using UnityEngine;

public class Farmer : EnemyBehavior
{
    public GameObject attackHitbox;
    public GameObject bullet;
    public Transform shotPoint;

    public float rangedCD;

    public int shotAmount;

    int bulletPoolIndex;
    bool inRangedCD;

    public void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
    }

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

    public void RangedAttack()
    {
        inRangedCD = true;
        Vector3 direction = enemyMovement.player.position - transform.position;
        shotPoint.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

        Shoot();
        Invoke(nameof(ResetRangedCD), rangedCD);
    }

    public void Shoot()
    {
        for (int i = 0; i < shotAmount; i++)
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
            bullet.GetComponent<Rigidbody2D>().AddForce(4f * Random.Range(10f, 12f) * DetermineSpread(), ForceMode2D.Force);
        }
    }

    public Vector2 DetermineSpread()
    {
        Vector2 spreadAngle = Quaternion.Euler(0, 0, Random.Range(-35f, 35f)) * transform.right;
        return spreadAngle;
    }

    public void ResetRangedCD() => inRangedCD = false;
    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}
