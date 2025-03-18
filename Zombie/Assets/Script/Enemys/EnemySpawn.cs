using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject zombie;
    public Transform zombieSpawnArea;
    public float spawnMax;
    public float currentSpawned;
    public float spawnDelay;
    int objectPoolNum;

    public bool hasSpawned;

    void Start()
    {
        objectPoolNum = ObjectPool.SharedInstance.GetObjectPoolNum(zombie);
    }

    public void FixedUpdate()
    {
        if (!hasSpawned && currentSpawned < spawnMax)
        {
            hasSpawned = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    public IEnumerator SpawnEnemy()
    {
        GameObject temp = ObjectPool.SharedInstance.GetPooledObject(objectPoolNum);
        if (temp == null)
            yield break;
        temp.SetActive(true);

        EnemyBehavior enemyBehavior = temp.GetComponent<EnemyBehavior>();
        enemyBehavior.InitializeStats(Random.Range(10f, 20f) / 10f, Random.Range(3f, 6f), 0.5f, Random.Range(5f, 8f));
        enemyBehavior.hasAttacked = false;
        
        temp.transform.position = zombieSpawnArea.position;
        yield return new WaitForSeconds(spawnDelay);
        hasSpawned = false;
    }
}
