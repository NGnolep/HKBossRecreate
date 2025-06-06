using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Assign your 3 enemy prefabs here in inspector
    public float initialSpawnInterval = 3f;
    public float minimumSpawnInterval = 0.5f;
    public float spawnIntervalDecrease = 0.1f;

    private float currentSpawnInterval;

    private void OnEnable()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(currentSpawnInterval);

            // Gradually shorten the interval but never below minimum
            currentSpawnInterval = Mathf.Max(minimumSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        int index = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[index], transform.position, Quaternion.identity);
    }
}
