using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;

    [Header("Stats")]
    public float damage;
    public float fireRate;
    public float bulletSpeed;
    public float shotAmount;
    public float clipSize;
    public float reloadTime;
    public float accuracy;
    public float clipAmount;

    internal float damageBuff, fireRateBuff, bulletSpeedBuff, shotAmountBuff, clipSizeBuff, reloadTimeBuff, accuracyBuff;

    public bool allowAuto, allowRaycast;
    int bulletPoolIndex;
    bool isReloading;
    bool shootCooldown;
    public LayerMask playerLayer, enemyLayer;

    void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
        clipAmount = clipSize;
    }

    void Update()
    {
        if ((allowAuto && Input.GetKey(KeyCode.E) || !allowAuto && Input.GetKeyDown(KeyCode.E)) && !shootCooldown)
        {
            if (clipAmount > 0)
            {
                shootCooldown = true;
                if (allowRaycast)
                {
                    RaycastShoot();
                }
                else
                {
                    ProjectileShoot();
                }
            }
            else if (!isReloading)
            {
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
    }

    public void ProjectileShoot()
    {
        for (int i = 0; i < shotAmount; i++)
        {
            clipAmount--;

            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletPoolIndex);

            if (bullet == null)
                break; 

            bullet.SetActive(true);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Transform bulletTransform = bullet.GetComponent<Transform>();
            bulletScript.damage = damage * damageBuff;
            bulletScript.layerToHit = enemyLayer;

            float spread = accuracy / accuracyBuff;
            bulletTransform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + UnityEngine.Random.Range(-spread, spread)));
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * bulletSpeedBuff * UnityEngine.Random.Range(8f, 12f) * bullet.transform.right, ForceMode2D.Force);

            StartCoroutine(bulletScript.Despawn());
        }
        StartCoroutine(ShootCD());
    }

    public void RaycastShoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 30f, ~enemyLayer);

        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent(out Health hitHealth))
                hitHealth.TakeDamage(damage * damageBuff);
                
        }
        StartCoroutine(ShootCD());
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(1 / fireRate * fireRateBuff);
        shootCooldown = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        clipAmount = clipSize;
        isReloading = false;
    }
}
