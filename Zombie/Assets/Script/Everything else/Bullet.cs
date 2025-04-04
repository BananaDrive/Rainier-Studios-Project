using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float despawnTimer;

    public LayerMask layerToHit, layerToDespawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerToHit & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent<Health>(out var health) && other.TryGetComponent<Rigidbody2D>(out var rb)) //checks if the collided object has a health script
            {
                Debug.Log(health);
                health.TakeDamage(damage);
                rb.AddForce(120 * transform.right, ForceMode2D.Force);
            }

            TurnOffObj();
        } 
    }

    void TurnOffObj()
    {
        layerToHit = 0;
        damage = 0f;
        gameObject.SetActive(false);
    }

    public IEnumerator Despawn()
    {
        float timer = 0;

        while (timer < despawnTimer) //makes a timer thatll cancel the script if the object is inactive
        {
            if (!gameObject.activeInHierarchy)
                yield break;

            timer += Time.deltaTime;
            yield return Time.deltaTime;
        }
        TurnOffObj();
    }
}
