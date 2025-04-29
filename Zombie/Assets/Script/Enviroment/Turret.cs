using System.Collections;
using UnityEngine;

public class Turret : Placeable
{
    public GameObject bullet;
    public float damage;
    public float attackRate;
    public float detectRadius;
    public bool hasAttacked;

    int bulletPoolIndex;

    public void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
    }
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
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletPoolIndex);

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.layerToHit = layerToHit;

        CoroutineHandler.Instance.StartCoroutine(bulletScript.Despawn());
        bullet.GetComponent<Transform>().position = transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(40f * transform.right, ForceMode2D.Force);

        CoroutineHandler.Instance.StartCoroutine(AttackCD());
    }

    public IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }
}
