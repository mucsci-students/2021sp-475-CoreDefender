using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreEnums;
public class Waypoint : MonoBehaviour
{
    public Direction nextDir;
    //Send the correct direction to move on to the enemy. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            switch(nextDir)
            {
                case Direction.UP:
                    collision.gameObject.GetComponent<BasicEnemy>().SetDirection("UP");
                    break;
                case Direction.DOWN:
                    collision.gameObject.GetComponent<BasicEnemy>().SetDirection("DOWN");
                    break;
                case Direction.LEFT:
                    collision.gameObject.GetComponent<BasicEnemy>().SetDirection("LEFT");
                    break;
                case Direction.RIGHT:
                    collision.gameObject.GetComponent<BasicEnemy>().SetDirection("RIGHT");
                    break;
            }
        }
    }
}
