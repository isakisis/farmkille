using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    public GameObject enemyRabbit;
    public GameObject grid;
    float time = 0;
    float spawnDelay = 4;
    int maxRabbits = 7;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        int numRabbits = 0;
        while(numRabbits <= maxRabbits)
        {
            Vector3 spawnVector = new Vector3(Random.Range(-9.5f, 9.5f), -5f, 0);
            var rabbit = Instantiate(enemyRabbit, spawnVector, Quaternion.identity);
            rabbit.transform.parent = grid.transform;
            numRabbits++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
