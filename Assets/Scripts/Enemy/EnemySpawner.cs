using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefabs to be spawned
    [Header("Enemies prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;

    // Time between spawn
    [Header("Configuration Variables")]
    [SerializeField] private float spawnInterval = 2f;

    // Reference to stop spawn
    private bool isSpawning = true;

    void Start()
    {
        // Beginning the cicle
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Infinite loop while spawn is active
        while (isSpawning)
        {
            // Check for assigned enemies
            if (enemyPrefabs.Length > 0)
            {
                // Choose a random prefab from the list
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyToSpawn = enemyPrefabs[randomIndex];

                // Instantiate the enemy in the spawner position
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }

            // Wait for the set time before the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StopSpawning()
    {
        // Allows you to stop spawning from another script
        isSpawning = false;
    }
}
