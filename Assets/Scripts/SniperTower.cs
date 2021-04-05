using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : MonoBehaviour, ITower
{
    private List<GameObject> targets = new List<GameObject>();
    public float fireCooldown;
    public GameObject projectileObj;
    public int towerCost;

    public float fireTimer = 0;
	public AudioSource audioSource;
    private bool fired = false;
    private float buffDamage;
    private bool isBuffed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(fired)
        {
            fireTimer += Time.deltaTime;
        }

        if (fireTimer > fireCooldown)
        {
            fireTimer = 0;
            fired = false;
        }

        if (targets.Count > 0 && !fired)
        {
            GameObject obj = FindNearestTarget();
            Fire(obj);
        }
    }

    //add creeps to the target list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }

    //remove creeps from target list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);
        }
    }

    //get the enemy nearest to the tower
    private GameObject FindNearestTarget()
    {
        Vector2 curTowerPos = transform.position;

        GameObject nearestEnemy = targets[targets.Count - 1];
        
        float curDistance = Vector2.Distance(curTowerPos, nearestEnemy.transform.position);

        foreach(GameObject go in targets)
        {
            Vector2 targetPos = go.transform.position;
            if(Vector2.Distance(curTowerPos, targetPos) < curDistance)
            {
                nearestEnemy = go;
            }
        }

        return nearestEnemy;
    }

    //fire on enemy

    public void Fire(GameObject objToFireOn)
    {
        GameObject projectile = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;
        BasicProjectile bp = projectile.GetComponent<BasicProjectile>();
        if (isBuffed)
        {
            bp.damage += buffDamage;
        }
        bp.Fire(transform.position, objToFireOn.transform.position);
		
		audioSource.Play();
		
        fired = true;
    }

    public int GetTowerCost()
    {
        return towerCost;
    }

    public bool GetIsBuffed()
    {
        return isBuffed;
    }

    public void AddDamageBuff(float dmgAmt)
    {
        buffDamage = dmgAmt;
    }

    public void SetIsBuffed(bool buffed)
    {
        isBuffed = buffed;
    }
}
