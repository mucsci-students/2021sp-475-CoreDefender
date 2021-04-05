using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTower : MonoBehaviour, ITower
{
    public int towerCost;
    public float damageBoost;
    private List<GameObject> placemntsInRange = new List<GameObject>();
    private bool isBuffed = true;
    public int GetTowerCost()
    {
        return towerCost;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindTowers();
    }

    // Update is called once per frame
    void Update()
    {
        FindTowers();
        ApplyBuff();
    }

    private void FindTowers()
    {
        foreach(TowerPlacement tp in FindObjectsOfType<TowerPlacement>())
        {
            float d = Vector2.Distance(transform.position, tp.gameObject.transform.position);

            if (tp.GetHasTower() && d < 2)
            {
                placemntsInRange.Add(tp.GetPlacedTower());
                tp.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
    }

    private void ApplyBuff()
    {
        foreach(GameObject go in placemntsInRange)
        {
            if(!go.GetComponent<ITower>().GetIsBuffed())
            {
                go.GetComponent<ITower>().AddDamageBuff(damageBoost);
                go.GetComponent<ITower>().SetIsBuffed(true);
            }
        }
    }

    public bool GetIsBuffed()
    {
        return isBuffed;
    }

    public void AddDamageBuff(float dmgAmt)
    {
        //void method here.
    }

    public void SetIsBuffed(bool buffed)
    {
        isBuffed = buffed;
    }
}
