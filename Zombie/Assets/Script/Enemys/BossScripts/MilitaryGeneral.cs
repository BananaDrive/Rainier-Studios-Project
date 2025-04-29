using UnityEngine;

public class MilitaryGeneral : EnemyBehavior
{
    public GameObject attackHitbox;
    public GameObject grenade;
    public Transform anchorPoint, preRangedHitbox, rangedHitbox;
    public Transform throwPoint;
    public Transform turretSpawns;
    public float grenadeAmount;

    public float rangedCD, grenadeCD, summonCD;
    bool inRangedCD, inGrenadeCD, inSummonCD;

    int grenadePoolNum;
    float throwPower;
    bool isAttacking;

    public void Start()
    {
        grenadePoolNum = ObjectPool.SharedInstance.GetObjectPoolNum(grenade);
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
                if (!inSummonCD)
                {
                    animator.SetInteger("State", 5);
                    return;
                }
                if (!inGrenadeCD)
                {
                    throwPower = 1.5f + Vector2.Distance(transform.position, enemyMovement.player.position) / 1.5f;
                    animator.SetInteger("State", 4);
                    return;
                }
                if (!inRangedCD)
                {
                    Vector3 direction = enemyMovement.player.position - transform.position;
                    anchorPoint.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
                    animator.SetInteger("State", 3);
                    return;
                }
                if (Vector2.Distance(transform.position, enemyMovement.player.position) <= enemyMovement.distanceToStop)
                    animator.SetInteger("State", 2);
            }
        }
    }


    public void PreRangedAttack()
    {
        inRangedCD = true;
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
    }

    public void SpawnTurrets()
    {
        inSummonCD = true;
        for (int i = 0; i < turretSpawns.childCount; i++)
        {
            turretSpawns.GetChild(i).GetComponent<Turret>().playerLocation = enemyMovement.player;
            turretSpawns.GetChild(i).gameObject.SetActive(true);
        }
        Invoke(nameof(SummonCD), summonCD);
    }

    public void GrenadeAttack()
    {
        inGrenadeCD = true;

        Vector2 throwDirection = (enemyMovement.player.position - transform.position).normalized;
        throwDirection.y += 0.75f;
        for (int i = 0; i < grenadeAmount; i++)
        {
            GameObject grenadeObj = ObjectPool.SharedInstance.GetPooledObject(grenadePoolNum);

            if (grenadeObj == null)
                return;
            grenadeObj.SetActive(true);

            grenadeObj.GetComponent<Grenade>().Intitialize(damage, enemyMovement.playerLayer);
            grenadeObj.transform.position = transform.position;

            Vector2 randomized = new(Random.Range(throwDirection.x * 0.75f, throwDirection.x * 1.25f), Random.Range(throwDirection.y * 0.75f, throwDirection.y * 1.25f));
            float randomizedPower = Random.Range(throwPower * 0.75f, throwPower * 1.25f);
            grenadeObj.GetComponent<Rigidbody2D>().AddForce(55f * randomizedPower * randomized, ForceMode2D.Force);
        }
        Invoke(nameof(GrenadeCD), grenadeCD);
    }


    void RangedCD() => inRangedCD = false;
    void SummonCD() => inSummonCD = false;
    void GrenadeCD() => inGrenadeCD = false;

    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}
