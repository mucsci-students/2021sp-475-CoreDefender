using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockTower : MonoBehaviour , ITower
{
    public float timer;
    public float fireCooldown;
    public float towerDamage;
    public int towerCost;
    public float fireTimer = 0;
	public AudioSource audioSource;
	
	
    private bool fired = false;
    private ParticleSystem part;
    private bool isBuffed = false;
    private float ogDmg;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        part.Play();
        ogDmg = towerDamage;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Enemy"))
        {
			audioSource.Play();
            other.GetComponent<BasicEnemy>().TakeDamage(towerDamage);
        }
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
        towerDamage = (dmgAmt * towerDamage) + towerDamage;
    }

    public void SetIsBuffed(bool buffed)
    {
        isBuffed = buffed;
    }
}
