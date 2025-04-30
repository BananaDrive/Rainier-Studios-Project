using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Turret : Placeable
{
    public GameObject bullet;
    public Transform shotPoint;
    public float damage;
    public float attackRate;
    public float despawnTime;
    public float detectRadius;
    public bool hasAttacked;

    public bool canAim;
    public Transform playerLocation;

    int bulletPoolIndex;

    public void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
    }

    void OnEnable()
    {
        Invoke(nameof(TurnOff), despawnTime);
    }
    public void FixedUpdate()
    {
        if (!hasAttacked)
        {
            hasAttacked = true;

            if (Physics2D.Raycast(transform.position, transform.right, detectRadius, layerToHit))
            {
                shotPoint.rotation = transform.rotation;
            }
            if (playerLocation != null)
            {
                Vector3 direction = playerLocation.position - transform.position;
                shotPoint.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
            }
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
        bullet.GetComponent<Transform>().position = shotPoint.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(40f * shotPoint.right, ForceMode2D.Force);

        CoroutineHandler.Instance.StartCoroutine(AttackCD());
    }

    public IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }

    public void TurnOff() => gameObject.SetActive(false);
}
