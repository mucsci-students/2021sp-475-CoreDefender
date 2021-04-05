using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreEnums;

public class EnemySpawner : MonoBehaviour
{

    public GameObject managerObj;
    public float spawnCooldown;
    
    public Direction startingDirection;
    public EnemyType enemyType;

    public GameObject levelBoss;
	
	public AudioSource music;
	public AudioSource bossMusic;

    /// </summary>
    private GameManager gm;
    private float cooldownTimer;
    private int spawnCounter = 0;
    private bool spawned = false;
    private bool allowedToSpawn = false;
    private int numCreepsPerWave;
    private int maxWaves;
    private bool bossHasSpawned = false;
	private bool alreadyPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        maxWaves = gm.totalWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer till next spawn
        if(spawned)
        {
            cooldownTimer += Time.deltaTime;
        }
        else if(allowedToSpawn)
        {
            SpawnEnemy(EnemyType.BASIC);
            ++spawnCounter;
            gm.AddToSpawnCounter();
        }

        if(cooldownTimer >= spawnCooldown)
        {
            cooldownTimer = 0;
            spawned = false;
        }

        if(spawnCounter >= numCreepsPerWave && !spawned && !bossHasSpawned)
        {
            SpawnEnemy(EnemyType.ELITE);
            spawnCounter = 0;
        }

        if(gm.GetCurrentWave() == maxWaves && !spawned && !bossHasSpawned)
        {
            SpawnEnemy(EnemyType.BOSS);
            bossHasSpawned = true;
        }
		
		if(bossHasSpawned && !alreadyPlayed) {
			music.Pause();
			bossMusic.Play();
			alreadyPlayed = true;
		}
    }

    public void SpawnEnemy(EnemyType type)
    {
        GameObject enemyToSpawn;
        switch (type)
        {
            case EnemyType.BASIC:
                enemyToSpawn = gm.GetBasicEnemy();
                break;
            case EnemyType.ELITE:
                enemyToSpawn = gm.GetEliteEnemy();
                break;
            case EnemyType.BOSS:
                enemyToSpawn = Instantiate(levelBoss);
                break;
            default:
                enemyToSpawn = gm.GetBasicEnemy();
                break;
        }
        
        enemyToSpawn.SetActive(true);

        if(type == EnemyType.BOSS)
        {
            enemyToSpawn.GetComponent<BasicEnemy>().SetAsBoss();
        }

        enemyToSpawn.transform.position = this.transform.position;
        int waveNum = gm.GetCurrentWave();

        if(waveNum > 1)
        {
            float curEnemySpeed = enemyToSpawn.GetComponent<BasicEnemy>().speed;
            enemyToSpawn.GetComponent<BasicEnemy>().speed = curEnemySpeed + ( curEnemySpeed * 0.02f);
            enemyToSpawn.GetComponent<BasicEnemy>().health += 0.06f;

        }
        enemyToSpawn.GetComponent<BasicEnemy>().Move();


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
        spawned = true;
    }

    public void AllowToSpawn()
    {
        allowedToSpawn = true;
    }

    public void DisallowSpawn()
    {
        allowedToSpawn = false;
    }

    public void SetCreepsPerWave(int creepsPerWave)
    {
        numCreepsPerWave = creepsPerWave;
    }

}
