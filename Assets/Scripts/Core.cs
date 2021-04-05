using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public GameObject manageObj;
    public GameObject[] towerChoices;
    public int health;
    public GameObject curSelectedTower;
	public HealthBar healthbar;
	
	public bool isGameOver = false;
	public GameObject gameOverScreen;

    private bool McAfee = false;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(health);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
		{
			isGameOver = true;
			endGame();
		}
    }

    public void TakeDamage(int damage)
    {
        if (McAfee == true)
            health -= (damage / 2);
        
        else
            health -= damage;
		
		healthbar.SetHealth(health);
    }

    public void YouAreNowProtected ()
    {
        McAfee = true;
    }

    public void YourSubscriptionHasExpired ()
    {
        McAfee = false;
    }

    public GameObject GetSelectedTower()
    {
        return curSelectedTower;
    }
	
	public void endGame()
	{
		gameOverScreen.SetActive(true);
		Time.timeScale = 0f;
	}

    //Temp way to select a tower
    public void PickTower(int choice)
    {
        curSelectedTower = towerChoices[choice];
    }
}
