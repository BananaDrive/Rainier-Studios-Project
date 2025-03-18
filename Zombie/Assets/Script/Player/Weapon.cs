using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public BuffsHandler buffs;

    [Header("Stats")]
    public float damage;
    public float fireRate;
    public float clipSize;
    public float bulletSpeed;
    public float shotAmount;
    public float reloadTime;

    public float clipAmount;

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
        if ((buffs.allowAuto && Input.GetKey(KeyCode.E) || !buffs.allowAuto && Input.GetKeyDown(KeyCode.E)) && !shootCooldown)
        {
            if (clipAmount > 0)
            {
                shootCooldown = true;
                if (buffs.allowRaycast)
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
        for (int i = 0; i < shotAmount + buffs.shotAmountEnhance; i++)
        {
            clipAmount--;

            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletPoolIndex);

            if (bullet == null)
                return;

            bullet.SetActive(true);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Transform bulletTransform = bullet.GetComponent<Transform>();
            bulletScript.damage = BuffCalculation(damage, buffs.damageEnhance, buffs.damageBuff);
            bulletScript.layerToHit = enemyLayer;

            float spread = 22.5f - (22.5f * buffs.accuracyEnhance / 100f);
            bulletTransform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + UnityEngine.Random.Range(-spread, spread)));
            bullet.GetComponent<Rigidbody2D>().AddForce(BuffCalculation(bulletSpeed, buffs.bulletSpeedEnhance, buffs.bulletSpeedBuff) * 10f * bullet.transform.right, ForceMode2D.Force);

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
                hitHealth.TakeDamage(BuffCalculation(damage, buffs.damageEnhance, buffs.damageBuff));
                
        }
        StartCoroutine(ShootCD());
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(1 / BuffCalculation(fireRate, buffs.fireRateEnhance, buffs.fireRateBuff));
        shootCooldown = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        clipAmount = clipSize;
        isReloading = false;
    }

    public float BuffCalculation(float mainStat, float enhancer, float itemBuff)
    {
        float tempValue = mainStat + (mainStat * enhancer / 100);
        return tempValue + (tempValue * itemBuff / 100);
    }
}
