using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTower : MonoBehaviour, ITower
{
    private List<GameObject> targets = new List<GameObject>();
    public float fireCooldown;
    public GameObject projectileObj;
    public int towerCost;

    public float fireTimer = 0;
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
        GameObject projectileCenter = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;
        GameObject projectileAdd1 = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;
        GameObject projectileSub1 = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;
        GameObject projectileAdd2 = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;
        GameObject projectileSub2 = Instantiate(projectileObj,transform.position,Quaternion.identity) as GameObject;

        BasicProjectile bpCenter = projectileCenter.GetComponent<BasicProjectile>();
        BasicProjectile bpAdd1 = projectileAdd1.GetComponent<BasicProjectile>();
        BasicProjectile bpSub1 = projectileSub1.GetComponent<BasicProjectile>();
        BasicProjectile bpAdd2 = projectileAdd2.GetComponent<BasicProjectile>();
        BasicProjectile bpSub2 = projectileSub2.GetComponent<BasicProjectile>();

        if(isBuffed)
        {
            bpCenter.damage += buffDamage;
            bpAdd1.damage += buffDamage;
            bpSub1.damage += buffDamage;
            bpAdd2.damage += buffDamage;
            bpSub2.damage += buffDamage;
        }

        Vector2 posAdd1 = objToFireOn.transform.position;
        Vector2 posSub1 = objToFireOn.transform.position;
        Vector2 posAdd2 = objToFireOn.transform.position;
        Vector2 posSub2 = objToFireOn.transform.position;

        posAdd1 [0] = GetVectorX(posAdd1) + .33f;
        posAdd1 [1] = GetVectorY(posAdd1) + .33f;

        posSub1 [0] = GetVectorX(posSub1) - .33f;
        posSub1 [1] = GetVectorY(posSub1) - .33f;

        posAdd2 [0] = GetVectorX(posAdd2) + .66f;
        posAdd2 [1] = GetVectorY(posAdd2) + .66f;

        posSub2 [0] = GetVectorX(posSub2) - .66f;
        posSub2 [1] = GetVectorY(posSub2) - .66f;

        bpCenter.Fire(transform.position, objToFireOn.transform.position);
        bpAdd1.Fire(transform.position, posAdd1);
        bpSub1.Fire(transform.position, posSub1);
        bpAdd2.Fire(transform.position, posAdd2);
        bpSub2.Fire(transform.position, posSub2);

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

    public float GetVectorX (Vector2 pos)
    {
        return pos [0];
    }

    public float GetVectorY (Vector2 pos)
    {
        return pos [1];
    }
}
