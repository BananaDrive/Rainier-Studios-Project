using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public LineRenderer lineRenderer;


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
    public Animator animator;
    public bool weaponReload = false;

    internal float damageBuff, fireRateBuff, bulletSpeedBuff, shotAmountBuff, clipSizeBuff, reloadTimeBuff, accuracyBuff;

    public bool allowAuto, allowRaycast, allowPiercing;
    int bulletPoolIndex;
    bool isReloading;
    bool shootCooldown;
    public LayerMask raycastLayer, enemyLayer;

    void Start()
    {
        bulletPoolIndex = ObjectPool.SharedInstance.GetObjectPoolNum(bullet);
        clipAmount = clipSize;
        GameManager.Instance.UIManager.ammoText.SetText(clipAmount + " / " + clipSize + clipSizeBuff);
    }

    void Update()
    {
        if ((allowAuto && Input.GetKey(KeyCode.E) || !allowAuto && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button1) || (allowAuto && Input.GetKey(KeyCode.Joystick1Button1))) && !shootCooldown)
        {
            if (clipAmount + clipSizeBuff > 0)
            {
                shootCooldown = true;
                for (int i = 0; i < shotAmount; i++)
                {
                    clipAmount--;
                    GameManager.Instance.UIManager.ammoText.SetText(clipAmount + " / " + (clipSize + clipSizeBuff));
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
                weaponReload = true;
                isReloading = true;
                StartCoroutine(Reload());
            }
        }

        GameManager.Instance.UIManager.ammoImage.fillAmount = (float)clipAmount / (float)(clipSize + clipSizeBuff);
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
        
        bulletTransform.SetPositionAndRotation(shootPoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * bulletSpeedBuff * Random.Range(8f, 12f) * DetermineSpread(), ForceMode2D.Force);

        StartCoroutine(bulletScript.Despawn());
    }

    public void RaycastShoot()
    {
        Vector3 tempHit;
        int pierceAmount = allowPiercing ? 30 : 1;
        float decayDamage = damage * damageBuff;

        Vector2 spread = DetermineSpread();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, spread, 50f, raycastLayer);

        if (hit.Length == 0)
            tempHit = shootPoint.position;
        
        else
            tempHit = hit[0].point;

        List<Vector3> transforms = new()
        {
            shootPoint.position,
            tempHit
        };

        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(transforms.ToArray());

        CancelInvoke(nameof(ClearLine));
        Invoke(nameof(ClearLine), 1.5f);

        for (int i = 0; i < pierceAmount && i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].transform.TryGetComponent(out Health hitHealth) && Vector2.Distance(hit[i].transform.position, transform.position) < 30f)
                {
                    decayDamage *= 0.75f + Mathf.Clamp(accuracy * accuracyBuff / 100f, 0, 0.2f);
                    hitHealth.TakeDamage(decayDamage);
                }
            }
        }
        StartCoroutine(ShootCD());
    }

    public Vector2 DetermineSpread()
    {
        float spread = Mathf.Clamp((100f - accuracy) / accuracyBuff, 0f, 100f);
        Debug.Log(100f - spread);
        Vector2 spreadAngle = Quaternion.Euler(0, 0, Random.Range(-spread, spread)) * transform.right;
        return spreadAngle;
    }

    public IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(1f / (fireRate * fireRateBuff));
        shootCooldown = false;
    }
    

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        clipAmount = clipSize + clipSizeBuff;
        isReloading = false;
    }

    public void ClearLine() => lineRenderer.positionCount = 0;
}
