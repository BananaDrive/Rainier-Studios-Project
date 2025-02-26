using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;

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
        Rigidbody2D bulletRb = Instantiate(bullet).GetComponent<Rigidbody2D>();
        bulletRb.GetComponent<Transform>().position = transform.position;
        bulletRb.AddForce(bulletSpeed * 10f * transform.right, ForceMode2D.Force);
        yield return new WaitForSeconds(fireRate);
        shootCooldown = false;
    }
}
