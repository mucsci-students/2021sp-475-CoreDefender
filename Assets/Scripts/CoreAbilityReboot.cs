using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAbilityReboot : MonoBehaviour
{
    private List<GameObject> listOfEnemies = new List<GameObject>();
    private bool used = false;
    private GameManager manager;
	private Currency mon;
	
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

    public void Reboot()
    {
		if(mon.getMoney() >= cost)
		{
			mon.Spend(cost);
			
			if(!used)
			{
				GetEnemyList();
				foreach (GameObject go in listOfEnemies)
				{
                    if (!go.GetComponent<BasicEnemy>().IsBoss())
					    manager.ReturnBasicEnemy(go);
				}
			}
		}
    }
}
