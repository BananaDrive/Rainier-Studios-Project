using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float despawnTimer;

    public LayerMask layerToIgnore;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerToIgnore & (1 << other.gameObject.layer)) != 0)
            return;

        if (other.TryGetComponent<Health>(out var health)) //checks if the collided object has a health script
            health.TakeDamage(damage);

        TurnOffObj();
    }

    void TurnOffObj()
    {
        layerToIgnore = 0;
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
