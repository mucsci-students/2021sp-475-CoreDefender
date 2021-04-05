using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
	public Text displayMoney;
	public int money = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayMoney.text = money.ToString();
    }
	
	public int getMoney() {
		return money;
	}
	
	public void Spend(int amount)
	{
		if((money - amount) <= 0)
		{
			money = 0;
		} else {
			money -= amount;
		}
	}
	
	public void Add(int amount)
	{
		money += amount;
	}
}
