using System.Collections;
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

    bool shootCooldown;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !shootCooldown) 
        {
            shootCooldown = true;
            StartCoroutine(Shoot()); //placeholder for shooting, will improve tmr
        } 
    }

    public IEnumerator Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        
        if (bullet != null)
        {
            bullet.SetActive(true);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Transform>().position = transform.position;
            bulletRb.AddForce(bulletSpeed * 10f * transform.right, ForceMode2D.Force);
            yield return new WaitForSeconds(fireRate);
            shootCooldown = false;
        }
    }
}
