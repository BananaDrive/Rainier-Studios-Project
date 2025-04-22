using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Serializable]
    public class ZombieSpawn
    {
        public GameObject zombie;
        public float spawnWeight;
    }
    public List<ZombieSpawn> zombies;
    public List<int> objectPoolNum;
    
    public Transform zombieSpawnArea;
    public float spawnMax;
    public float currentSpawned;
    public float spawnDelay;

    public bool hasSpawned;
    public float totalSpawnWeight;

    void Start()
    {
        foreach (ZombieSpawn zombieSpawn in zombies)
        {
            objectPoolNum.Add(ObjectPool.SharedInstance.GetObjectPoolNum(zombieSpawn.zombie));
            totalSpawnWeight += zombieSpawn.spawnWeight;
        }
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
        GameObject temp = ObjectPool.SharedInstance.GetPooledObject(EnemyToSpawn());
        if (temp == null)
            yield break;
        temp.SetActive(true);

        EnemyBehavior enemyBehavior = temp.GetComponent<EnemyBehavior>();
        enemyBehavior.InitializeStats(UnityEngine.Random.Range(3f, 6f), UnityEngine.Random.Range(5f, 8f));
        enemyBehavior.hasAttacked = false;
        
        temp.transform.position = zombieSpawnArea.position;
        yield return new WaitForSeconds(spawnDelay);
        hasSpawned = false;
    }

    public int EnemyToSpawn()
    {
        float temp = UnityEngine.Random.Range(0, totalSpawnWeight);
        for (int i = 0; i < zombies.Count - 1; i++)
        {
            temp -= zombies[i].spawnWeight;
            if (temp <= 0)
                return objectPoolNum[i];
        }
        return objectPoolNum[^1];
    }
}
