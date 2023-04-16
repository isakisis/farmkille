using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    public GameObject enemyRabbit;
    float time = 0;
    float startDelay = 4;

    void Start()
    {
        
    }

    IEnumerator EnemySpawn()
    {
        Boolean spawnEnemies = true
        while(spawnEnemies)
        {
            Vector3 spawnVector = new Vector3(Random.Range(-10f, 10f), -6, 0);
            Instantiate(enemyRabbit, spawnVector, Quaternion.identity);
        }
    }
}
