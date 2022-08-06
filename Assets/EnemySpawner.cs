using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int spawnIndex;
    public GameObject enemy;

    [SerializeField] List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy()
    {
        spawnIndex = Random.Range(0, spawnPoints.Count - 1);

        GameObject newEnemy = Instantiate(enemy, spawnPoints[spawnIndex].position, gameObject.transform.rotation);
        newEnemy.GetComponentInChildren<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;

    }
}
