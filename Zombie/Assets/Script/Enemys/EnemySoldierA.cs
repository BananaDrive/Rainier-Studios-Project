using UnityEngine;

public class EnemySoldierA: EnemyRanged
{
    void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            RangedAttack();
        }
        EnableClip<EnemyRanged>();
    }
}
