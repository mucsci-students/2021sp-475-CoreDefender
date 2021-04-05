using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject coreObj;
    public int totalWaves;
    public int totalCreepsPerWave;
	public Text displayWave;
	
	public int selectedTower;
	public int selectedAbility;

    private EnemySpawner[] spawners;
    private EnemyPool pools;
    private Core core;
    private bool coreActive = false;
    private int curWave;
    private int spawnCounter;
    private Scene activeScene;

    // Start is called before the first frame update
    void Start()
    {
        core = FindObjectOfType<Core>();
        pools = GetComponent<EnemyPool>();
        pools.BuildEnemyPools();
        coreActive = true;
        spawners = FindObjectsOfType<EnemySpawner>();

        foreach (EnemySpawner es in spawners)
        {
            es.AllowToSpawn();
            es.SetCreepsPerWave(totalCreepsPerWave);
            curWave = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coreActive)
        {
            displayWave.text = "" + curWave;

            if (spawnCounter >= (totalCreepsPerWave * spawners.Length))
            {
                curWave += 1;
                spawnCounter = 0;
                foreach (EnemySpawner es in spawners)
                {
                    if (es.spawnCooldown > 0.5)
                    {
                        es.spawnCooldown -= 0.15f;
                    }
                }
            }
        }
    }
	
	public void LoadMenu ()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void LoadGame0 ()
	{
		SceneManager.LoadScene("Level_1");
	}
	public void LoadGame1 ()
	{
		SceneManager.LoadScene("Level_2");
	}
	public void LoadGame2 ()
	{
		SceneManager.LoadScene("Level_3");
	}
	public void LoadCredits ()
	{
		SceneManager.LoadScene("Credits 1");
	}

    public void LoadWinScene()
    {
        SceneManager.LoadScene("LevelWin");
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void exitgame()
	{
		Debug.Log("exitgame");
		Application.Quit();
	}
	
	public void resetTimeScale()
	{
		Time.timeScale = 1f;
	}
    
    public GameObject GetBasicEnemy()
    {
        return pools.GetBasicEnemy();
    }

    public GameObject GetEliteEnemy()
    {
        return pools.GetEliteEnemy();
    }

    public void ReturnBasicEnemy(GameObject enemy)
    {
        pools.ReturnBasicEnemy(enemy);
    }

    public void ReturnEliteEnemy(GameObject enemy)
    {
        pools.ReturnEliteEnemy(enemy);
    }

    public void AddToSpawnCounter()
    {
        spawnCounter += 1;

    }
	
	public void setAbility(int ab) 
	{
		selectedAbility = ab;
	}
	
	public void setTower(int t) 
	{
		selectedTower = t;
        if(core == null)
        {
            core = FindObjectOfType<Core>();
        }
        core.PickTower(t);
	}

    public int GetCurrentWave()
    {
        return curWave;
    }

    public void LevelWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print("Level Complete!");
    }

    public void SetCoreActive(bool active)
    {
        coreActive = active;
    }
}
