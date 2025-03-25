using System.Collections;
using UnityEngine;

public class Turret : Placeable
{
    public float damage;
    public float attackRate;
    public float detectRadius;

    public bool hasAttacked;
    public void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, transform.right, detectRadius, layerToHit) && !hasAttacked)
        {
            hasAttacked = true;
            RangedAttack();
        }  
    }
    public void RangedAttack()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(0);

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.layerToHit = layerToHit;

        StartCoroutine(bulletScript.Despawn());
        bullet.GetComponent<Transform>().position = transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(40f * transform.right, ForceMode2D.Force);
    }

    public IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }
}
