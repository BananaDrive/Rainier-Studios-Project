using System;
using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.Burst;
using UnityEngine;

public class Weapon : MonoBehaviour
{

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
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();

        if (bullet == null)
            return;

        bullet.SetActive(true);
        
        bullet.GetComponent<Transform>().position = transform.position;
        bullet.GetComponent<Bullet>().damage = damage;
        StartCoroutine(bullet.GetComponent<Bullet>().Despawn());
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * 10f * transform.right, ForceMode2D.Force);

        StartCoroutine(ShootCD());
    }

    public void RaycastShoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 30f, ~playerLayer);

        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent(out Health hitHealth))
                hitHealth.TakeDamage(damage);
        }
        StartCoroutine(ShootCD());
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(fireRate);
        shootCooldown = false;
    }
}
