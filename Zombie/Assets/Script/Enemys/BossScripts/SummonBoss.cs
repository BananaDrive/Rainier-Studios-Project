using UnityEngine;

public class SummonBoss : MonoBehaviour
{
    public GameObject boss;
    bool hasSummoned;

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!hasSummoned)
        {
            hasSummoned = true;
            boss.SetActive(true);
        }
    }
}
