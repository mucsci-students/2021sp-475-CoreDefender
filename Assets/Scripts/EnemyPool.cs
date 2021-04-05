using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private const int MAX_ENEMIES = 75;
    private const int MAX_ELITE_ENEMIES = 20;

    public GameObject[] basicEnemies;
    public GameObject[] eliteEnemies;


    //Queues to hold enemies. 
    private Queue<GameObject> basicEnemyPool = new Queue<GameObject>();
    private Queue<GameObject> eliteEnemyPool = new Queue<GameObject>();

    public void BuildEnemyPools()
    {
        for (int numOfTypes = 0; numOfTypes < basicEnemies.Length; numOfTypes++)
        {
            for (int i = 0; i < MAX_ENEMIES; ++i)
            {
                GameObject spawnedEnemy = Instantiate(basicEnemies[numOfTypes]) as GameObject;
                spawnedEnemy.SetActive(false);
                basicEnemyPool.Enqueue(spawnedEnemy);
            }
        }

        for (int numOfTypes = 0; numOfTypes < eliteEnemies.Length; numOfTypes++)
        {
            for (int i = 0; i < MAX_ENEMIES; ++i)
            {
                GameObject spawnedEnemy = Instantiate(eliteEnemies[numOfTypes]) as GameObject;
                spawnedEnemy.SetActive(false);
                eliteEnemyPool.Enqueue(spawnedEnemy);
            }
        }
    }

    public GameObject GetBasicEnemy()
    {
        return basicEnemyPool.Dequeue();
    }

    public GameObject GetEliteEnemy()
    {

        return eliteEnemyPool.Dequeue(); 
    }

    public void ReturnBasicEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        basicEnemyPool.Enqueue(enemy);
    }

    public void ReturnEliteEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        eliteEnemyPool.Enqueue(enemy);
    }
}
