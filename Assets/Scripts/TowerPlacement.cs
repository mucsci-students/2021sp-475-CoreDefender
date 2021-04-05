using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
	public Currency mon;
    private SpriteRenderer sr;
    private Color originalColor;
    public GameObject coreObj;
    private Core core;
    private bool hasTower = false;
    private GameObject placedTower;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        core = coreObj.GetComponent<Core>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        originalColor = sr.material.color;
        GameObject selectedTower = core.GetSelectedTower();
        int towerPrice = selectedTower.GetComponent<ITower>().GetTowerCost();

        if (!hasTower && mon.getMoney() >= towerPrice)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        sr.color = originalColor;
    }

    private void OnMouseDown()
    {
        //if the player has selected a tower. 
        GameObject selectedTower = core.GetSelectedTower();
        int towerPrice = selectedTower.GetComponent<ITower>().GetTowerCost();
        if(selectedTower != null && !hasTower && mon.getMoney() >= towerPrice && !PauseMenu.GameIsPaused)
        {
            placedTower = Instantiate(selectedTower, transform.position, Quaternion.identity);
            hasTower = true;
			mon.Spend(towerPrice);
        }
        //TODO consider adding popup window here to ask if they are sure?
        //and no tower is in this slot
        //place a tower
    }

    public bool GetHasTower()
    {
        return hasTower;
    }

    public GameObject GetPlacedTower()
    {
        return placedTower;
    }
}
