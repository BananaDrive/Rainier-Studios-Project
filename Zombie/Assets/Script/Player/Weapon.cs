using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public AudioSource gun;

    internal float damageBuff, fireRateBuff, bulletSpeedBuff, shotAmountBuff, clipSizeBuff, reloadTimeBuff, accuracyBuff;

    public bool allowAuto, allowRaycast, allowPiercing;
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
        var gamepad = Gamepad.current;
        bool controller = false;
        if (gamepad != null)
            controller = true;
        Debug.DrawRay(transform.position, transform.right);
        if (allowAuto && Input.GetKey(KeyCode.E) || (!allowAuto && (Input.GetKeyDown(KeyCode.E) || (controller && gamepad.buttonWest.wasPressedThisFrame)) && !shootCooldown ))
        {
            if (clipAmount > 0)
            {
                shootCooldown = true;
                for (int i = 0; i < shotAmount; i++)
                {
                    clipAmount--;
                    if (allowRaycast)
                        RaycastShoot();
                    else
                        ProjectileShoot();
                    if (clipAmount <= 0)
                        break;
                    gun.Play();
                }
                StartCoroutine(ShootCD());
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
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletPoolIndex);

        if (bullet == null)
            return;

        bullet.SetActive(true);
            
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Transform bulletTransform = bullet.GetComponent<Transform>();
        bulletScript.damage = damage * damageBuff;
        bulletScript.layerToHit = enemyLayer;
        bulletScript.isPiercing = allowPiercing;
        
        bulletTransform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * bulletSpeedBuff * Random.Range(8f, 12f) * DetermineSpread(), ForceMode2D.Force);

        StartCoroutine(bulletScript.Despawn());
    }

    public void RaycastShoot()
    {
        int pierceAmount = allowPiercing ? 30 : 1;
        Vector2 spread = DetermineSpread();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, spread, 30f, enemyLayer);
        float decayDamage = damage * damageBuff;

        for (int i = 0; i < pierceAmount && i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].transform.TryGetComponent(out Health hitHealth))
                {
                    decayDamage *= 0.75f;
                    hitHealth.TakeDamage(decayDamage);
                }
              
            }
        }
        StartCoroutine(ShootCD());
    }

    public Vector2 DetermineSpread()
    {
        float spread = 100f - (accuracy + accuracyBuff);
        Vector2 spreadAngle = Quaternion.Euler(0, 0, Random.Range(-spread, spread)) * transform.right;
        return spreadAngle;
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(1f / (fireRate * fireRateBuff * 2f));
        shootCooldown = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        clipAmount = clipSize;
        isReloading = false;
    }
}
