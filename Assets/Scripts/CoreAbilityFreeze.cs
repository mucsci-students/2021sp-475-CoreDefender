using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAbilityFreeze : MonoBehaviour
{
    private List<GameObject> listOfEnemies = new List<GameObject>();
    private bool used = false;
    private GameManager manager;
	private Currency mon;

    public GameObject spawner1;
    public GameObject spawner2;
	public int cost;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
		mon = GameObject.FindObjectOfType<Currency>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void GetEnemyList()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy") )
        {
            listOfEnemies.Add(go);
        }
    }

    public void Freeze()
    {
		if(mon.getMoney() >= cost)
		{
			mon.Spend(cost);
			
			spawner1.GetComponent<EnemySpawner>().DisallowSpawn();
			spawner2.GetComponent<EnemySpawner>().DisallowSpawn();

			GetEnemyList();
			
			foreach (GameObject go in listOfEnemies)
			{
				float OriginalSpeed = go.GetComponent<BasicEnemy>().speed;
		
				go.GetComponent<BasicEnemy>().speed = 0;

				StartCoroutine(waitThreeSeconds(go, OriginalSpeed));
			}
		}
    }

    IEnumerator waitThreeSeconds(GameObject go, float OS) 
    {
        yield return new WaitForSeconds(3);

        foreach(GameObject e in listOfEnemies)
        {
            e.GetComponent<BasicEnemy>().speed = OS;
        }

        spawner1.GetComponent<EnemySpawner>().AllowToSpawn();
        spawner2.GetComponent<EnemySpawner>().AllowToSpawn();
    }
}