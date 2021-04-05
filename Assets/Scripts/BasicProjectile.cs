using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float damage;
    private bool fired = false;
    public float speed = 6;
    private Vector2 startPos;
    private Vector2 endPos;

    public bool AOE;
    private bool AOEDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fired)
        {
            Vector2 dir = endPos - startPos;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }

    public void Fire(Vector2 start, Vector2 end)
    {
        startPos = start;
        endPos = end;
        fired = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (AOE == true)
            {
                AOEDamage = true;
            }

            BasicEnemy e = collision.GetComponent<BasicEnemy>();
            e.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public bool CheckAOE ()
    {
        return AOEDamage;
    }
}
