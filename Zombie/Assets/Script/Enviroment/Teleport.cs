using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player.position = endPoint.position;

    }
}
