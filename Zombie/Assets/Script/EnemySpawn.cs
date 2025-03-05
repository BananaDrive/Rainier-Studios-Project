using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform zombieSpawnArea;

    public void FixedUpdate()
    {
        GameObject temp = ObjectPool.SharedInstance.GetPooledObject(1);
        if (temp == null)
            return;
        temp.SetActive(true);
        temp.GetComponent<EnemyBehavior>().InitializeStats(Random.Range(10f, 20f) / 10f, Random.Range(3f, 6f), 0.5f, Random.Range(5f, 8f));
        temp.transform.position = zombieSpawnArea.position;
    }
}
