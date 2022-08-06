using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int spawnIndex;

    [SerializeField] List<EnemySpawner> enemySpawners;

    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            spawnIndex = Random.Range(0, enemySpawners.Count - 1);

            enemySpawners[spawnIndex].SpawnEnemy();

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
