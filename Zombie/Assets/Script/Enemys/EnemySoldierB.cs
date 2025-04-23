using UnityEngine;

public class EnemySoldierB : EnemyBehavior
{
    public GameObject molotov;
    Vector2 throwDirection;
    float throwPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        animator.SetInteger("State", 0);

        if (enemyMovement.moveDirection != 0)
            animator.SetInteger("State", 1);
        
        if (enemyMovement.player != null)
        {
            if (Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
            {
                animator.SetInteger("State", 2);
                throwDirection = (enemyMovement.player.position - transform.position).normalized;
                throwPower = Vector2.Distance(transform.position, enemyMovement.player.position);
            }
        }
        EnableClip<EnemyRanged>();
    }

    public void RangedAttack()
    {
        if (interrupted)
            return;
        GameObject molotovObj = Instantiate(molotov);

        if (molotov == null)
            return;

        molotovObj.SetActive(true);
        
        Molotov _molotov = molotovObj.GetComponent<Molotov>();
        _molotov.damage = damage;
        _molotov.layerToHit = enemyMovement.playerLayer;

        StartCoroutine(_molotov.Despawn());
        molotovObj.GetComponent<Transform>().SetPositionAndRotation(transform.position, transform.rotation);
        molotovObj.GetComponent<Rigidbody2D>().AddForce(135f * throwPower * throwDirection, ForceMode2D.Force);
    }

    public Vector2 SetAngle()
    {
        return Quaternion.Euler(0f, 0f, 30f * (enemyMovement.transform.eulerAngles.y > 0 ? -1 : 1)) * transform.right; 
    }
}
