using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEArea : MonoBehaviour
{
    public GameObject projectile;
    public float damage;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.GetComponent<BasicProjectile>().CheckAOE() == true)
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add (collision.gameObject);
        }
    }

    private void OnTriggerLeave2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove (collision.gameObject);
        }
    }
    
    private void DealDamage()
    {
        foreach (GameObject e in enemies)
        {
            e.GetComponent<BasicEnemy>().TakeDamage(damage);
        }

        Destroy (this.gameObject);

        //BasicEnemy e = collision.GetComponent<BasicEnemy>();
        //e.TakeDamage(damage);
        //Destroy(this.gameObject);
    }
}
