using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int health = 5;
    public int maxHealth = 5;
    public int damageGiven = 1;
    public int damageRecieved = 1;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerBullets")
        {
            health -= damageRecieved;
            Destroy(other.gameObject);
        }
    }
}
