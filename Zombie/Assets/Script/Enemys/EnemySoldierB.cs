using UnityEngine;

public class EnemySoldierB : EnemyBehavior
{
    public GameObject molotov;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            RangedAttack();
        }
        EnableClip<EnemyRanged>();
    }

    public void RangedAttack()
    {
        GameObject molotovObj = Instantiate(molotov);

        if (molotov == null)
            return;

        molotovObj.SetActive(true);
        
        Molotov _molotov = molotovObj.GetComponent<Molotov>();
        _molotov.damage = damage;
        _molotov.layerToHit = enemyMovement.playerLayer;

        StartCoroutine(_molotov.Despawn());
        molotovObj.GetComponent<Transform>().SetPositionAndRotation(transform.position, transform.rotation);
        molotovObj.GetComponent<Rigidbody2D>().AddForce(400f * SetAngle(), ForceMode2D.Force);

        StartCoroutine(AttackCD());
    }

    public Vector2 SetAngle()
    {
        return Quaternion.Euler(0f, 0f, 30f * (enemyMovement.transform.eulerAngles.y > 0 ? -1 : 1)) * transform.right; 
    }
}
