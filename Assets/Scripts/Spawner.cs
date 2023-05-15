using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] public List<Enemy> spawnerList;
    [SerializeField] private GameObject boss;
    [SerializeField] private float spawnDelay;
    [SerializeField] private Vector3 spawnArea;
    [SerializeField] private TMP_Text enemiesLeft;
    private bool bossHasSpawned = false;

    private Dictionary<GameObject, Enemy> spawnedEnemies = new Dictionary<GameObject, Enemy>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnerList.Count; i++)
        {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            0f,
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        // Get the enemy at the current index of the list
        Enemy enemyToSpawn = spawnerList[i];

        // Instantiate the enemy at the spawn position
        GameObject spawn = Instantiate(enemyToSpawn.gameObject, spawnPosition, Quaternion.identity);

        // Add the spawned enemy to the dictionary
        spawnedEnemies.Add(spawn, enemyToSpawn);

        // Wait for the spawn delay
        yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        // Check if any spawned enemies have been destroyed, and remove their corresponding enemies from the list
        List<GameObject> toRemove = new List<GameObject>();
        foreach (KeyValuePair<GameObject, Enemy> pair in spawnedEnemies)
        {
            if (pair.Key == null)
            {
                toRemove.Add(pair.Key);
                spawnerList.Remove(pair.Value);
            }
        }

        foreach (GameObject key in toRemove)
        {
            spawnedEnemies.Remove(key);
        }
        enemiesLeft.text = $"Enemies Left:{spawnerList.Count}";
        if(spawnerList.Count == 0)
        {
            StartCoroutine(SpawnBoss());

        }
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(spawnDelay);
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            0f,
            Random.Range(-spawnArea.z, spawnArea.z)
            );
        if(bossHasSpawned == false)
            {
                GameObject bosspawn = Instantiate(boss, spawnPosition, Quaternion.identity);
                bossHasSpawned = true;
                Debug.Log("Spawning Boss");
                enemiesLeft.text = $"Big Bug";
            }
        
    }
}

