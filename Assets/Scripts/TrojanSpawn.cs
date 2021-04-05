using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreEnums;

public class TrojanSpawn : MonoBehaviour
{
    public GameObject managerObj;
    public int spawnCooldown;
    public Direction startingDirection;
    BasicEnemy enemy;
    private GameManager gm;
    private float cooldownTimer;
    //private bool spawned = false;
    //private bool allowedToSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        enemy = GetComponent<BasicEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

        //Timer till next spawn
        /*if(spawned)
        {
            cooldownTimer += Time.deltaTime;
        }
        else if(allowedToSpawn)
        {
            SpawnEnemy();
        }

        if(cooldownTimer >= spawnCooldown)
        {
            cooldownTimer = 0;
            spawned = false;
        }*/
    }

    public void SpawnEnemy()
    {
        print("Hi, spawn enemy called.");
        GameObject enemyToSpawn = gm.GetEliteEnemy();
        enemyToSpawn.SetActive (true);
        
        enemyToSpawn.transform.position = transform.position;
        
        switch (startingDirection)
        {
            case Direction.UP:
                enemyToSpawn.GetComponent<BasicEnemy>().SetDirection("UP");
                break;
            case Direction.DOWN:
                enemyToSpawn.GetComponent<BasicEnemy>().SetDirection("DOWN");
                break;
            case Direction.LEFT:
                enemyToSpawn.GetComponent<BasicEnemy>().SetDirection("LEFT");
                break;
            case Direction.RIGHT:
                enemyToSpawn.GetComponent<BasicEnemy>().SetDirection("RIGHT");
                break;
        }
        enemyToSpawn.GetComponent<BasicEnemy>().Move();
        //spawned = true;
    }
}
