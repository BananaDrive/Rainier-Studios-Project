using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BuffsHandler buffs;

    [Header("Stats")]
    public float damage;
    public float fireRate;
    public float clipSize;
    public float bulletSpeed;
    public bool isRaycast;

    bool shootCooldown;
    public LayerMask playerLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !shootCooldown)
        {
            if (isRaycast)
            {
                shootCooldown = true;
                RaycastShoot();
            }
            else
            {
                shootCooldown = true;
                ProjectileShoot();
            }
        } 
    }

    public void ProjectileShoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(0);

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.GetComponent<Transform>().position = transform.position;
        bulletScript.damage = damage + (damage * buffs.damageBuff / 100);
        bulletScript.layerToIgnore = playerLayer;
        StartCoroutine(bulletScript.Despawn());
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * 10f * transform.right, ForceMode2D.Force);

        StartCoroutine(ShootCD());
    }

    public void RaycastShoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 30f, ~playerLayer);

        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent(out Health hitHealth))
                hitHealth.TakeDamage(damage + (damage * buffs.damageBuff / 100));
        }
        StartCoroutine(ShootCD());
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(1 / (fireRate + (fireRate * buffs.fireRateBuff / 100)));
        shootCooldown = false;
    }
}
