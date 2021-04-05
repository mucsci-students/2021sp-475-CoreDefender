using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreEnums;
public class BasicEnemy : MonoBehaviour
{

    public float speed;
    public int damageAmt;
    public bool isMoving;
    public int startingHealth;
    public float health;
    public GameObject gmObj;

    public bool isILY;

    private Direction currentDirection;
    private GameManager gm;
	private Currency mon;
    private bool isBoss = false;

    // Start is called before the first frame update
    void Start()
    {
		mon = GameObject.FindObjectOfType<Currency>();
        gm = GameObject.FindObjectOfType<GameManager>();
        //currentDirection = Direction.RIGHT;
        health = startingHealth;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if(isBoss)
            {
                gm.LevelWin();
            }
			mon.Add(15);
            ResetHealth();
            gm.ReturnBasicEnemy(this.gameObject);

            if(GetComponent<TrojanSpawn>())
            {
                GetComponent<TrojanSpawn>().SpawnEnemy();
            }
        }

        if (isILY)
        {
            ilyHealthChange();
        }

        if(isMoving)
        {
            Vector2 dir = new Vector2(0, 0);

            switch (currentDirection)
            {
                case Direction.UP:
                    dir = Vector2.up;
                    break;
                case Direction.DOWN:
                    dir = Vector2.down;
                    break;
                case Direction.LEFT:
                    dir = -Vector2.right;
                    break;
                case Direction.RIGHT:
                    dir = Vector2.right;
                    break;
            }
            this.transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    public void Move()
    {
        isMoving = true;
    }

    public void SetDirection(string dir)
    {
        switch(dir)
        {
            case "UP":
                currentDirection = Direction.UP;
                break;
            case "DOWN":
                currentDirection = Direction.DOWN;
                break;
            case "LEFT":
                currentDirection = Direction.LEFT;
                break;
            case "RIGHT":
                currentDirection = Direction.RIGHT;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Core"))
        {
            Core core = collision.gameObject.GetComponent<Core>();
            core.TakeDamage(damageAmt);
            gm.ReturnBasicEnemy(this.gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    public void ResetHealth()
    {
        health = startingHealth;
    }

    public void ilyHealthChange()
    {
        if (startingHealth != health)   
            speed += (startingHealth - health) * 0.0001f;
    }

    public void SetAsBoss()
    {
        isBoss = true;
    }

    public bool IsBoss()
    {
        return isBoss;
    }
}
